using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    public bool isEntrance;
    private GroundSpawner groundSpawner;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isEntrance)
            {
                groundSpawner.SpawnTile();
            }
            else
            {
                Destroy(transform.parent.gameObject, 2f);
            }
        }
    }
}