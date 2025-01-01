using UnityEngine;
using TMPro; // Dodaj t? lini? na pocz?tku

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // Pr?dko?? ruchu
    public float xMin = -10.0f, xMax = 10.0f; // Granice ruchu w osi X
    public float yMin = -5.0f, yMax = 5.0f; // Granice ruchu w osi Y

    public TextMeshProUGUI scoreText; // Wy?wietlanie punktów
    public TextMeshProUGUI timeText; // Wy?wietlanie czasu

    private int score = 0; // Liczba punktów gracza
    private float elapsedTime = 0.0f; // Up?ywaj?cy czas

    void Update()
    {
        // Pobranie wej?cia gracza
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Obliczenie wektora ruchu
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // Przesuni?cie obiektu
        transform.Translate(movement * speed * Time.deltaTime);

        // Ograniczenie pozycji gracza w granicach ekranu
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedY = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // Aktualizacja czasu
        elapsedTime += Time.deltaTime;
        UpdateTimeUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdzenie, czy obiekt, z którym dosz?o do kolizji, to "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");
            Destroy(gameObject); // Usuni?cie gracza
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Gracz unika przeszkody
        if (other.CompareTag("Enemy"))
        {
            score++;
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateTimeUI()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime) + "s";
        }
    }
}



