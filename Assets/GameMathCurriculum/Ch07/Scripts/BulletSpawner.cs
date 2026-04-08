using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform startPoint;
    public Transform endPoint;
    [Range(1, 100)] public int bulletCount = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullets();
        }
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Init(endPoint.position);
        }
    }
}