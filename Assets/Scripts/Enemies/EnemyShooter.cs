using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject spikePrefab;

    public Transform firePoint;
    public Transform endPoint;

    public float fireRate = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        GameObject spike = Instantiate(
            spikePrefab,
            firePoint.position,
            Quaternion.identity);

        spike.GetComponent<SpikeProjectile>().SetTarget(endPoint);
    }
}