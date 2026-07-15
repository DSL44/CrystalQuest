using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text coinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public GameObject gameOverText;

    public TMP_Text diamondText;

    private int diamonds = 0;

    private int coins = 0;
    private int lives = 3;

    [Header("Dano")]
    public PlayerController player;
    public float knockbackForce = 8f;
    public AudioSource audioSource;
    public AudioClip hurtSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameOverText.SetActive(false);

        UpdateUI();
    }

    public void AddCoin()
    {
        coins++;
        UpdateUI();
    }

    public void AddDiamond()
    {
        diamonds++;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (lives <= 0)
            return;

        lives--;

        // Som de dano
        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        // Empurra o jogador para trás
        if (player != null)
        {
            Vector2 direction = player.transform.localScale.x > 0
                ? Vector2.left
                : Vector2.right;

            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().AddForce(
                (direction + Vector2.up * 0.5f) * knockbackForce,
                ForceMode2D.Impulse);
        }

        UpdateUI();

        if (lives <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        gameOverText.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        coinText.text = "Moedas: " + coins;
        diamondText.text = "Diamantes: " + diamonds;

        heart1.enabled = lives >= 1;
        heart2.enabled = lives >= 2;
        heart3.enabled = lives >= 3;
    }
}