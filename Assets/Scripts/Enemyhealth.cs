using UnityEngine;

public class Enemyhealth : Health
{
    public static int LivingEnemyCount = 0;

    private void Awake() => LivingEnemyCount++;

    private void Update()
    {
       Debug.Log("Number of enemy is: " +  LivingEnemyCount);
    }

    protected override void Die()
    {
    LivingEnemyCount--;
    base.Die();
    Debug.Log("Enemy died");
    }

    public void KillEnemy()
    {
        Die();
    }
}
