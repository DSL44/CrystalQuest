using UnityEngine;

public class SwingTrigger : MonoBehaviour
{
    public EnemyTeleport enemy;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            enemy.started = true;
        }
    }
}