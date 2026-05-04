using UnityEngine;
using System.Collections; 

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3.0f;
    public float moveSpeed = 15f;

    [Header("Configurações de Pulo")]
    public float jumpForce = 8f;
    public float gravity = -25f;

    [Header("Configurações de Agachar")]
    public float crouchHeightScale = 0.5f; 
    public float crouchDuration = 1.5f;    
    private bool isCrouching = false;
    private float originalHeight;         

    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1;

    private Vector2 startTouchPosition;
    private float minSwipeDistance = 50f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; 
    }

    void Update()
    {
        HandleInput();

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * moveSpeed;

        if (controller.isGrounded)
        {
            if (direction.y < 0) direction.y = -1f;
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        moveVector.y = direction.y;
        moveVector.z = forwardSpeed;

        controller.Move(moveVector * Time.deltaTime);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) MoveLane(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) MoveLane(-1);
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && controller.isGrounded) Jump();
        if (Input.GetKeyDown(KeyCode.S) && !isCrouching) StartCoroutine(Crouch());

        if (Input.GetMouseButtonDown(0)) startTouchPosition = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endTouchPosition = Input.mousePosition;
            AnalyzeSwipe(endTouchPosition);
        }
    }

    private void AnalyzeSwipe(Vector2 endPos)
    {
        Vector2 swipeDelta = endPos - startTouchPosition;

        if (swipeDelta.magnitude > minSwipeDistance)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0) MoveLane(1);
                else MoveLane(-1);
            }
            else
            {
                if (y > 0 && controller.isGrounded) Jump(); 
                else if (y < 0 && !isCrouching) StartCoroutine(Crouch()); 
            }
        }
    }

    private void MoveLane(int step)
    {
        desiredLane += step;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    IEnumerator Crouch()
    {
        isCrouching = true;

        controller.height = originalHeight * crouchHeightScale;
        controller.center = new Vector3(0, controller.height / 2f, 0);
        transform.localScale = new Vector3(transform.localScale.x, crouchHeightScale, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + (controller.height / 2f), transform.position.z);
        yield return new WaitForSeconds(crouchDuration);

        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        controller.height = originalHeight;
        controller.center = new Vector3(0, originalHeight / 2f, 0);

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        isCrouching = false;
    }

}