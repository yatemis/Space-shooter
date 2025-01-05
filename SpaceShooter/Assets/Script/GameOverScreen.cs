using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Pobierz wynik z poprzedniej sceny
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Wy?wietl wynik
        scoreText.text = "Score: " + finalScore;
    }

    public void RestartGame()
    {
        // Za?aduj scen? gry ponownie
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
