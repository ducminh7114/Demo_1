using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public float shootingInterval;
    private float lastBulletTime;
    public Vector3 bulletOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateFiring();
    }

    private void updateFiring()
    {
        if (Time.time - lastBulletTime > shootingInterval)
        {
            ShootBullet();
            lastBulletTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        Quaternion downwardRotation = Quaternion.Euler(90f, 0f, 0f);
        var bullet = Instantiate(bulletPrefabs, transform.position + bulletOffset, transform.rotation);

    }
}
