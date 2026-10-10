using UnityEngine;

public class Enemyhealth : Health
{
    public static int LivingEnemyCount;

    private void Awake() => LivingEnemyCount++;

    protected override void Die()
    {
    LivingEnemyCount--;
    base.Die();
    Debug.Log("Enemy died");
    }
}
