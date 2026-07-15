using UnityEngine;

public class Diamond : MonoBehaviour
{
    public MoveBird bird;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Soma o diamante
        GameManager.Instance.AddDiamond();

        // Só chama o pássaro se ele existir
        if (bird != null)
        {
            bird.Attack(other.transform);
        }

        Destroy(gameObject);
    }
}