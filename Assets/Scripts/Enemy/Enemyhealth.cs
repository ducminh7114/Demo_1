using UnityEngine;

public class Enemyhealth : Health
{
    public static int LivingEnemyCount = 0;

    private void Awake()
    {
        LivingEnemyCount++;
    }

    protected override void Die()
    {
        if (isDead)
            return;

        LivingEnemyCount--;

        Debug.Log("Enemy died. Remaining enemies: " + LivingEnemyCount);

        base.Die();
    }

    public void KillEnemy()
    {
        Die();
    }

    private void OnDestroy()
    {
        // Optional: useful if enemies can also be destroyed
        // by other scripts or by manually removing them.
    }
}