using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab przeciwnika
    public float spawnInterval = 10.0f; // Interwa? mi?dzy falami przeciwników
    public float xMin = -10.0f, xMax = 10.0f; // Granice w osi X, gdzie przeciwnicy mog? si? pojawi?
    public float spawnY = 10.0f; // Wysoko??, na której przeciwnicy si? pojawiaj?
    public int enemiesPerWave = 5; // Liczba przeciwników na fal?

    public TextMeshProUGUI scoreText; // Wy?wietlanie punktów
    private int score = 0; // Liczba punktów gracza

    private bool isSecondWave = false; // Flaga dla drugiej fali
    private int activeEnemies = 0; // Liczba aktywnych przeciwników

    void Start()
    {
        // Rozpocz?cie cyklicznego spawnowania przeciwników
        SpawnFirstWave();
    }

    void SpawnFirstWave()
    {
        isSecondWave = false;
        SpawnWave();
        Invoke("SpawnSecondWave", spawnInterval); // Uruchomienie drugiej fali po czasie
    }

    void SpawnSecondWave()
    {
        isSecondWave = true;
        SpawnWave();
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            float randomX = Random.Range(xMin, xMax);
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies++;

            // Dodajemy callback do przeciwników, aby zmniejsza? licznik aktywnych przeciwników
            enemy.GetComponent<EnemyMovement>().OnEnemyDestroyed += HandleEnemyDestroyed;
        }

        AddScore();
    }

    private void AddScore()
    {
        score += isSecondWave ? 2 : 1; // Dodanie dodatkowych punktów za drug? fal?
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void HandleEnemyDestroyed()
    {
        activeEnemies--;

        // Sprawdzenie, czy wszystkie fale zosta?y zako?czone
        if (activeEnemies <= 0)
        {
            Invoke("SpawnFirstWave", spawnInterval); // Rozpocz?cie nowego cyklu fal
        }
    }
}

