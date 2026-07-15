using UnityEngine;
using UnityEngine.SceneManagement;

public class Bat : MonoBehaviour
{
    public float speed = 5f;
    public float followTime = 8f;

    private Transform player;

    void OnEnable()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;

        Invoke(nameof(Disappear), followTime);
    }

    void Update()
    {
        if (player == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}