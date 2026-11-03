using UnityEditor.PackageManager;
using UnityEngine;

public class Enemyhealth : Health
{
    public static int LivingEnemyCount;
    private ObjectReplacement objectReplacement;

    private void Awake()
    {
        LivingEnemyCount++;
        objectReplacement = GetComponent<ObjectReplacement>();
        ResetLivingEnemyCount();
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

        if(LivingEnemyCount < 0)
        {
            Debug.Log("Number of enemy reach 0");
            LivingEnemyCount = 0;
        }

        if (objectReplacement != null)
        {
            objectReplacement.TryReplace();
        }

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