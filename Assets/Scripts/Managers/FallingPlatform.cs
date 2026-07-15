using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Tempo")]
    public float waitBeforeFall = 1f;
    public float waitOnGround = 2f;
    public float automaticFallTime = 8f;

    [Header("Velocidade")]
    public float fallSpeed = 15f;
    public float returnSpeed = 5f;

    [Header("Queda")]
    public float fallDistance = 8f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool playerOnPlatform = false;
    private bool falling = false;
    private bool activated = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.down * fallDistance;

        StartCoroutine(AutoFallRoutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        playerOnPlatform = true;

        if (!activated)
        {
            activated = true;

            StartCoroutine(FallRoutine());
            StartCoroutine(AutoFallRoutine());
        }
    }
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnPlatform = false;
    }

    IEnumerator FallRoutine()
    {
        if (falling)
            yield break;

        falling = true;

        yield return new WaitForSeconds(waitBeforeFall);

        // Se o jogador saiu antes do tempo, cancela a queda
        if (!playerOnPlatform)
        {
            falling = false;
            yield break;
        }

        // Cai
        while (Vector3.Distance(transform.position, targetPosition) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                fallSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(waitOnGround);

        // Volta
        while (Vector3.Distance(transform.position, startPosition) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                returnSpeed * Time.deltaTime);

            yield return null;
        }

        falling = false;
    }

    IEnumerator AutoFallRoutine()
{
    while (true)
    {
        yield return new WaitForSeconds(8f);

        if (!falling)
        {
            StartCoroutine(AutoFall());
        }
    }
}
IEnumerator AutoFall()
{
    falling = true;

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

    falling = false;
}
}