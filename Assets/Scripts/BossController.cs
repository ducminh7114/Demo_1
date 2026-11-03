
using UnityEngine;
using System.Collections;
using System.Net;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public Health bossHealth;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player;

    [Header("Entrance Movement")]
    public Transform startPoint;
    public Transform endPoint;
    public float entranceSpeed = 3f;

    [Header("Circular Movement")]
    public float circleRadiusX = 2f;
    public float circleRadiusY = 0.8f;
    public float circleSpeed = 1f;

    [Header("Shooting")]
    public float attackInterval = 2f;
    public float bulletSpeed = 5f;
    public int bulletDamage = 10;

    [Header("Spread Attack")]
    public int bulletCount = 5;
    public float spreadAngle = 15f;

    private Vector3 centerPosition;
    private float circleTimer;

    private int currentPhase = 1;
    private bool bossDead = false;

    private bool entranceComplete = false;

    private void Start()
    {
        if (bossHealth == null)
            bossHealth = GetComponent<Health>();

        bossHealth.onDead += BossDefeated;

        if (startPoint != null)
            transform.position = startPoint.position;

        StartCoroutine(BossEntrance());
    }

    private void Update()
    {
        if (bossDead) return;

        CircularMovement();
        UpdateBossPhase();
    }

    IEnumerator BossEntrance()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Boss Start Point or End Point missing!");
            yield break;
        }

        transform.position = startPoint.position;

        while (Vector3.Distance(
            transform.position,
            endPoint.position) > 0.05f)
        {
            if (bossDead) yield break;

            transform.position = Vector3.MoveTowards(
                transform.position,
                endPoint.position,
                entranceSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = endPoint.position;

        centerPosition = endPoint.position;

        entranceComplete = true;

        Debug.Log("Boss entrance completed!");

        StartCoroutine(BossAttack());
    }

    void CircularMovement()
    {
        circleTimer += Time.deltaTime * circleSpeed;

        float x = Mathf.Cos(circleTimer) * circleRadiusX;
        float y = Mathf.Sin(circleTimer) * circleRadiusY;

        transform.position = centerPosition +
            new Vector3(x, y, 0);
    }


    void UpdateBossPhase()
    {
        if (bossHealth == null) return;

        float hp = (float)bossHealth.healthPoint /
                   bossHealth.defaultHealthPoint;

        if (hp <= 0.3f)
            currentPhase = 3;

        else if (hp <= 0.6f)
            currentPhase = 2;

        else
            currentPhase = 1;
    }

    IEnumerator BossAttack()
    {
        while (!bossDead)
        {
            float waitTime = attackInterval;

            if (currentPhase == 2)
                waitTime *= 0.75f;

            if (currentPhase == 3)
                waitTime *= 0.5f;

            yield return new WaitForSeconds(waitTime);

            if (bossDead || player == null || firePoint == null)
                continue;

            switch (currentPhase)
            {
                case 1:
                    ShootSpread(3, 15f);
                    break;

                case 2:
                    ShootSpread(5, 15f);
                    break;

                case 3:
                    ShootSpread(9, 12f);
                    yield return new WaitForSeconds(0.3f);
                    ShootSpread(9, 12f);
                    break;
            }
        }
    }

    void ShootSpread(int count, float angle)
    {
        Vector2 direction =
            (player.position - firePoint.position).normalized;

        float startAngle =
            -angle * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            float currentAngle =
                startAngle + angle * i;

            Vector2 bulletDirection =
                Quaternion.Euler(0, 0, currentAngle) * direction;

            SpawnBullet(bulletDirection);
        }
    }

    void SpawnBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        enemyBullet bulletScript =
            bullet.GetComponent<enemyBullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);

            bulletScript.flySpeed = bulletSpeed;
            bulletScript.damage = bulletDamage;
        }
        else
        {
            Debug.LogError(
                "Bullet prefab is missing enemyBullet script!"
            );
        }
    }


    void BossDefeated()
    {
        bossDead = true;
        StopAllCoroutines();

        Debug.Log("BOSS DEFEATED!");
    }

    private void OnDestroy()
    {
        if (bossHealth != null)
            bossHealth.onDead -= BossDefeated;
    }
}
