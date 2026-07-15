using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        float scale = Mathf.Abs(Mathf.Sin(Time.time * speed));

        transform.localScale = new Vector3(scale, 1f, 1f);
    }
}
