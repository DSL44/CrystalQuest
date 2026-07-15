using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    public float upDistance = 0.5f;
    public float speed = 2f;

    public float waitUp = 1f;
    public float waitDown = 2f;

    private Vector3 downPos;
    private Vector3 upPos;

    void Start()
    {
        downPos = transform.position;
        upPos = downPos + Vector3.up * upDistance;

        StartCoroutine(SpikeRoutine());
    }

    IEnumerator SpikeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitDown);

            while (Vector3.Distance(transform.position, upPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    upPos,
                    speed * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(waitUp);

            while (Vector3.Distance(transform.position, downPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    downPos,
                    speed * Time.deltaTime);

                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.LoseLife();

        other.transform.position = new Vector3(0, -2, 0);
    }
}