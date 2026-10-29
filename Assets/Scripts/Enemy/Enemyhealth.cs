using UnityEngine;

public class Enemyhealth : Health
{
    public static int LivingEnemyCount;

    private void Awake()
    {
        LivingEnemyCount++;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetLivingEnemyCount()
    {
        LivingEnemyCount = 0;
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