using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    public float minY = 0f;
    public float maxY = 100f;

    void LateUpdate()
    {
        if (target == null)
            return;

        float y = Mathf.Clamp(target.position.y, minY, maxY);

        Vector3 desiredPosition = new Vector3(
            target.position.x,
            y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}