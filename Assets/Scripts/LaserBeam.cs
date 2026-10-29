using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer line;
    public float range = 10f;
    public int damage = 1;
    public float damageInterval = 0.2f;
    public LayerMask hitLayers;

    private float nextDamageTime;

    private void Awake()
    {
       if(line == null)
        {
            line = GetComponent<LineRenderer>();

            line.positionCount = 2;
            line.enabled = false;
        }
    }

    public void Fire(Vector2 origin, Vector2 direction)
    {
        line.enabled = true;
        line.SetPosition(0, origin);

        RaycastHit2D hit = Physics2D.Raycast(
            origin, direction, range, hitLayers
        );

        Vector2 endPoint = origin + direction * range;

        if (hit.collider != null)
        {
            endPoint = hit.point;

            if (Time.time >= nextDamageTime)
            {
                Enemyhealth enemy =
                    hit.collider.GetComponent<Enemyhealth>();

                if (enemy != null)
                    enemy.TakeDamage(damage);

                nextDamageTime = Time.time + damageInterval;
            }
        }

        line.SetPosition(1, endPoint);
    }

    public void StopFiring()
    {
        if (line != null)
        {
            line.enabled = false;
        }
    }
}
