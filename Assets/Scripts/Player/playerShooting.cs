using UnityEngine;

public class playerShooting : MonoBehaviour
{
    [Header("Prefabs")]
    public Bullet normalBulletPrefab;
    public Bullet spreadBulletPrefab;

    [Header("Laser")]
    public LaserBeam laserBeamPrefab;
    private LaserBeam laserBeam;


    [Header("Bullet type")]
    public BulletType currentBulletType = BulletType.Normal;

    [Header("shooting setting")]
    public float shootingInterval;
    public Vector3 bulletOffset;
    public int spreadBulletCount = 5;
    public float spreadAngle = 60f;

    private float lastBulletTime;
    public enum BulletType
    {
        Normal,
        Spread,
        Laser
    }

    public void SelectNormal()
    {
        currentBulletType = BulletType.Normal;
    }

    public void SelectSpread()
    {
        currentBulletType = BulletType.Spread;
    }

    public void SelectLaser()
    {
        currentBulletType = BulletType.Laser;
    }

    private void Awake()
{
    if (laserBeamPrefab != null)
    {
        laserBeam = Instantiate(laserBeamPrefab);
        laserBeam.StopFiring();
    }
}

    public void SetBulletType(BulletType newType)
    {
        currentBulletType = newType;
        if (newType != BulletType.Laser && laserBeam != null)
        {
            laserBeam.StopFiring();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Hold to shoot
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Button Pressed");
            if (currentBulletType == BulletType.Laser)
            {
                if (laserBeam != null)
                {
                    Vector2 direction = transform.up;
                    Vector2 origin = transform.position + bulletOffset;

                    Debug.Log("Lazer shoot");
                    laserBeam.Fire(origin, direction);
                }
            }
            else if (Time.time - lastBulletTime >= shootingInterval)
            {
                Debug.Log("Bullet shoot");
                ShootBullet();
                lastBulletTime = Time.time;
            }
        }
        else if (laserBeam != null)
        {
            laserBeam.StopFiring();
        }
    }

    private void ShootBullet()
    {
        if (currentBulletType == BulletType.Normal)
        {
            SpawnBullet(normalBulletPrefab, transform.up);
        }
        else if (currentBulletType == BulletType.Spread)
        {
            ShootSpread();
        }
    }

    private void ShootSpread()
    {
        if (spreadBulletPrefab == null || spreadBulletCount <= 0)
            return;

        for (int i = 0; i < spreadBulletCount; i++)
        {
            float angle = 0f;

            if (spreadBulletCount > 1)
            {
                angle = -spreadAngle / 2f
                    + spreadAngle * i / (spreadBulletCount - 1);
            }

            Vector2 direction =
                Quaternion.Euler(0, 0, angle) * transform.up;

            SpawnBullet(spreadBulletPrefab, direction);
        }
    }

    private void SpawnBullet(Bullet prefab, Vector2 direction)
    {
        if (prefab == null)
            return;

        Vector3 spawnPosition = transform.position + bulletOffset;

        Bullet bullet = Instantiate(
            prefab, spawnPosition, Quaternion.identity
        );

        bullet.Initialize(direction);
    }
}
