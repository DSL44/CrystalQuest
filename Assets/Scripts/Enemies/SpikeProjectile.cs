using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    public float speed = 8f;

    private Transform target;

    public void SetTarget(Transform endPoint)
    {
        target = endPoint;
    }

    void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Colidiu com: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log(other.tag);

        GameManager.Instance.LoseLife();

        other.transform.position = new Vector3(0, -2, 0);

        Destroy(gameObject);
    }
}
}