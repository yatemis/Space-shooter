using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip powerUpSound; // D?wi?k odtwarzany przy zebraniu power-upa
    private AudioSource audioSource;

    private void Start()
    {
        // Dodaj komponent AudioSource, je?li go nie ma
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Wy??cz automatyczne odtwarzanie
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Sprawd?, czy gracz dotkn?? power-upa
        {
            // Odtwórz d?wi?k
            PlaySound();

            // Aktywuj spowolnienie w EnemySpawner
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.ActivateSlowNextWave();
            }

            // Zniszcz power-up po odtworzeniu d?wi?ku
            Destroy(gameObject, powerUpSound.length); // Usu? obiekt po zako?czeniu d?wi?ku
            Debug.Log("Power-up collected! Next wave will be slowed.");
        }
    }

    private void PlaySound()
    {
        if (powerUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }
        else
        {
            Debug.LogWarning("Power-up sound or AudioSource is missing!");
        }
    }
}
