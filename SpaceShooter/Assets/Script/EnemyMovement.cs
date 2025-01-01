using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f; // Pr?dko?? ruchu przeciwnika

    // Dodanie zdarzenia do obs?ugi zniszczenia przeciwnika
    public delegate void EnemyDestroyedHandler();
    public event EnemyDestroyedHandler OnEnemyDestroyed;

    void Update()
    {
        // Poruszanie przeciwnika w dó?
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Usuni?cie przeciwnika, je?eli opu?ci ekran
        if (transform.position.y < -10)
        {
            DestroyEnemy();
        }
    }

    private void OnDestroy()
    {
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke(); // Wywo?anie zdarzenia
        }
        Destroy(gameObject);
    }
}

