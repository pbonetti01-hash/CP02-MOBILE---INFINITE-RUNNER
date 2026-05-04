using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Pool de Peças de Chão")]
    public GameObject[] groundTiles;

    private Vector3 nextSpawnPoint;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    public void SpawnTile()
    {
        int randomIndex = Random.Range(0, groundTiles.Length);
        SpawnTile(randomIndex);
    }

 
    public void SpawnTile(int index)
    {
        if (groundTiles[index] == null) return;

        GameObject temp = Instantiate(groundTiles[index], nextSpawnPoint, Quaternion.identity);

        Transform nextPoint = temp.transform.Find("NextSpawnPoint");

        if (nextPoint != null)
        {
            nextSpawnPoint = nextPoint.position;
        }
        else
        {
            Debug.LogError("O prefab " + groundTiles[index].name + " está sem o objeto NextSpawnPoint!");

            nextSpawnPoint += new Vector3(0, 0, 15);
        }
    }
}