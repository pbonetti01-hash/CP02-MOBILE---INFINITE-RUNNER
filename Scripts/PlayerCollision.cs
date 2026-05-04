using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI do Jogo")]
    [SerializeField] private GameObject gameOverCanvas; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("O Jogador Morreu!");

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}