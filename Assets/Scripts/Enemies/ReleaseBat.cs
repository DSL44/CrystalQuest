using UnityEngine;

public class ReleaseBat : MonoBehaviour
{
    public GameObject bat;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        if (other.CompareTag("Vaso"))
        {
            activated = true;

            bat.SetActive(true);
        }
    }
}