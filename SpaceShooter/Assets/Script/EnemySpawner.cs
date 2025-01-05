using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab; // Prefab power-upa
    public float spawnInterval = 10.0f;
    public float xMin = -10.0f, xMax = 10.0f, yMin = -10.0f, yMax = 10.0f;
    public int startingEnemiesPerWave = 5;
    public TextMeshProUGUI scoreText;

    private int currentWave = 0;
    private int score = 0;
    private int activeEnemies = 0;
    private bool isSpawningWave = false;
    private bool waveCompleted = false;
    private bool slowNextWave = false;

    // Enum dla kierunków fal
    private enum SpawnDirection
    {
        TopToBottom,
        BottomToTop,
        LeftToRight,
        RightToLeft
    }

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (isSpawningWave) return;

        isSpawningWave = true;
        waveCompleted = false;
        currentWave++;
        int enemiesInThisWave = startingEnemiesPerWave + (currentWave - 1);

        // Losuj kierunek dla fali
        SpawnDirection direction = GetRandomSpawnDirection();
        Debug.Log($"Spawning wave {currentWave} with {enemiesInThisWave} enemies. Direction: {direction}");

        for (int i = 0; i < enemiesInThisWave; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(direction);
            Vector3 moveDirection = GetMoveDirection(direction);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies++;

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                if (slowNextWave)
                {
                    enemyMovement.speed *= 0.5f; // Zmniejsz pr?dko?? przeciwników
                }

                enemyMovement.SetMoveDirection(moveDirection);
                enemyMovement.OnEnemyDestroyed += HandleEnemyDestroyed;
            }
        }

        slowNextWave = false; // Resetuj flag? po zastosowaniu spowolnienia
    }

    private SpawnDirection GetRandomSpawnDirection()
    {
        // Losuj kierunek z dost?pnych opcji
        SpawnDirection[] directions = { SpawnDirection.TopToBottom, SpawnDirection.BottomToTop, SpawnDirection.LeftToRight, SpawnDirection.RightToLeft };
        return directions[Random.Range(0, directions.Length)];
    }

    private Vector3 GetSpawnPosition(SpawnDirection direction)
    {
        // Ustaw pozycj? spawnów w zale?no?ci od kierunku fali
        switch (direction)
        {
            case SpawnDirection.TopToBottom:
                return new Vector3(Random.Range(xMin, xMax), yMax, 0);
            case SpawnDirection.BottomToTop:
                return new Vector3(Random.Range(xMin, xMax), yMin, 0);
            case SpawnDirection.LeftToRight:
                return new Vector3(xMin, Random.Range(yMin, yMax), 0);
            case SpawnDirection.RightToLeft:
                return new Vector3(xMax, Random.Range(yMin, yMax), 0);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetMoveDirection(SpawnDirection direction)
    {
        // Ustaw kierunek ruchu przeciwników w zale?no?ci od kierunku fali
        switch (direction)
        {
            case SpawnDirection.TopToBottom:
                return Vector3.down;
            case SpawnDirection.BottomToTop:
                return Vector3.up;
            case SpawnDirection.LeftToRight:
                return Vector3.right;
            case SpawnDirection.RightToLeft:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    private void SpawnPowerUp()
    {
        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);
        Vector3 spawnPosition = new Vector3(randomX, randomY, -1);

        Debug.Log($"Trying to spawn power-up at {spawnPosition}");

        if (powerUpPrefab != null)
        {
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Power-up spawned!");
        }
        else
        {
            Debug.LogError("PowerUp prefab is not assigned in the inspector!");
        }
    }

    private void HandleEnemyDestroyed()
    {
        activeEnemies--;

        if (activeEnemies <= 0 && !waveCompleted)
        {
            AddScoreForWave();
            waveCompleted = true;
            isSpawningWave = false;

            if (currentWave % 2 == 0)
            {
                SpawnPowerUp();
            }

            Invoke("StartNextWave", spawnInterval);
        }
    }

    private void AddScoreForWave()
    {
        score += 10;
        UpdateScoreUI();
        Debug.Log($"Wave {currentWave} completed. Total score: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void ActivateSlowNextWave()
    {
        Debug.Log("ActivateSlowNextWave called: Slowing next wave!");
        slowNextWave = true;
    }
}
