using UnityEngine;

public class FlyPathAgent : MonoBehaviour
{
    public FlyPath flyPath;
    public float flySpeed = 3f;
    public Enemyhealth enemyhealth;

    private int nextIndex = 1;

    private void Awake()
    {
        if (enemyhealth == null)
            enemyhealth = GetComponent<Enemyhealth>();
    }

    private void Update()
    {
        if (flyPath == null || flyPath.waypoints == null ||
            flyPath.waypoints.Length == 0)
            return;

        if (nextIndex >= flyPath.waypoints.Length)
        {
            if (enemyhealth != null)
                enemyhealth.KillEnemy();

            return;
        }

        Vector3 target = flyPath[nextIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            flySpeed * Time.deltaTime
        );

        LookAt(target);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            nextIndex++;
        }
    }

    private void LookAt(Vector2 destination)
    {
        Vector2 position = transform.position;
        Vector2 lookDirection = destination - position;

        if (lookDirection.magnitude < 0.01f)
            return;

        float angle = Vector2.SignedAngle(
            Vector2.down,
            lookDirection
        );

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}