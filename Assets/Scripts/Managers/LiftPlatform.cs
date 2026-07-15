using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform topPoint;

    private Vector3 startPosition;
    private bool playerOnPlatform = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                topPoint.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                speed * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        playerOnPlatform = true;

        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        playerOnPlatform = false;

        collision.transform.SetParent(null);
    }
}