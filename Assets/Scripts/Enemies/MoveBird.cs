using UnityEngine;

public class MoveBird : MonoBehaviour
{
    public float speed = 7f;

    private Transform player;
    private bool attacking = false;

    void Update()
    {
        if (!attacking || player == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) < 0.2f)
        {
            GameManager.Instance.LoseLife();

            player.position = new Vector3(0, -2, 0);

            Destroy(gameObject);
        }
    }

    public void Attack(Transform target)
    {
        player = target;

        transform.position = player.position + Vector3.up * 5f;

        attacking = true;

        gameObject.SetActive(true);
    }
}