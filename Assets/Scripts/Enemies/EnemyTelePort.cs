using UnityEngine;
using System.Collections;

public class EnemyTeleport : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float visibleTime = 2f;
    public float hiddenTime = 1.5f;

    private SpriteRenderer sr;
    private Collider2D col;

    public bool started = false;
    public bool stopTeleport = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Hide();

        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            if (!started || stopTeleport)
            {
                Hide();
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(hiddenTime);

            int index = Random.Range(0, spawnPoints.Length);

            transform.position = spawnPoints[index].position;

            Show();

            yield return new WaitForSeconds(visibleTime);

            Hide();
        }
    }

    void Show()
    {
        sr.enabled = true;
        col.enabled = true;
    }

    void Hide()
    {
        sr.enabled = false;
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Encostou em: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Matou!");

            GameManager.Instance.LoseLife();
        }
    }
}