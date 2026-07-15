using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        GameManager.Instance.AddCoin();

        // Esconde a moeda
        spriteRenderer.enabled = false;
        col.enabled = false;

        // Toca o som
        audioSource.Play();

        // Destrói a moeda quando o som terminar
        Destroy(gameObject, audioSource.clip.length);
    }
}