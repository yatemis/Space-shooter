using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0;

    public void AddScore(int points)
    {
        playerScore += points;
    }

    public void EndGame()
    {
        // Przechowaj wynik gracza mi?dzy scenami
        PlayerPrefs.SetInt("FinalScore", playerScore);

        // Za?aduj ekran ko?cowy
        SceneManager.LoadScene("GameOver");
    }
}
