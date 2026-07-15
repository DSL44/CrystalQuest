using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FallingSpikeTrap : MonoBehaviour
{
    [Header("Movimento")]
    public float fallSpeed = 20f;
    public float returnSpeed = 3f;

    [Header("Tempos")]
    public float waitBeforeFall = 2f;
    public float waitOnGround = 1f;

    [Header("Distância da queda")]
    public float fallDistance = 6f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.down * fallDistance;

        StartCoroutine(TrapRoutine());
    }

    IEnumerator TrapRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitBeforeFall);

            while (Vector3.Distance(transform.position, targetPosition) > 0.02f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    fallSpeed * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(waitOnGround);

            while (Vector3.Distance(transform.position, startPosition) > 0.02f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    startPosition,
                    returnSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}