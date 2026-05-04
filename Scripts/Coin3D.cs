using UnityEngine;

public class Coin3D : MonoBehaviour
{
    [Header("Configurações")]
    public float rotationSpeed = 100f;

    [Header("Áudio")]
    public AudioClip coinSound;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoin();
        }

        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        Destroy(gameObject);
    }
}