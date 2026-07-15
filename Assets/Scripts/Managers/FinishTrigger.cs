using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public EnemyTeleport enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.stopTeleport = true;
        }
    }
}
