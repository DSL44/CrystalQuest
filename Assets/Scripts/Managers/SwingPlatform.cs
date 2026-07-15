using UnityEngine;

public class SwingPlatform : MonoBehaviour
{
    public float angle = 25f;
    public float speed = 1f;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        float rotation = Mathf.Sin(Time.time * speed) * angle;

        transform.rotation =
            startRotation * Quaternion.Euler(0, 0, rotation);
    }
}