using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 moveDirection = Vector3.down;

    public delegate void EnemyDestroyedHandler();
    public event EnemyDestroyedHandler OnEnemyDestroyed;

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (OutOfBounds())
        {
            DestroyEnemy();
        }
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized; // Normalizuj kierunek
    }

    private bool OutOfBounds()
    {
        return transform.position.y < -15 || transform.position.y > 15 ||
               transform.position.x < -15 || transform.position.x > 15;
    }

    private void DestroyEnemy()
    {
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke();
        }
        Destroy(gameObject);
    }
}
