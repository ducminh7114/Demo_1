using UnityEngine;

public class Enemyhealth : Health
{
    protected override void Die()
    {
    base.Die();
    Debug.Log("Enemy died");
    }
}
