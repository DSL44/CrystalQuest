using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;
    private bool facingRight = true;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime);

        // Quando chegar perto do ponto, troca o destino
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            if (target == pointA)
            {
                target = pointB;
            }
            else
            {
                target = pointA;
            }

            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.localScale = new Vector3(
            facingRight ? 1 : -1,
            1,
            1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
            collision.transform.position = new Vector3(0, -2, 0);
        }
    }
}