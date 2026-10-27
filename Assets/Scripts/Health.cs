using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint;

    public Action onDead;
    public Action onHealthChanged;

    [HideInInspector] public int healthPoint;

    protected bool isDead = false;

    private void Start()
    {
        healthPoint = defaultHealthPoint;
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || healthPoint <= 0)
            return;

        healthPoint -= damage;
        onHealthChanged?.Invoke();

        if (healthPoint <= 0)
        {
            Die();
        }
    }

    public void HealPlayer(int damage)
    {
        if(isDead || healthPoint <= 0)
        {
            return;
        }
        healthPoint += damage;
        onHealthChanged?.Invoke();

        if(healthPoint == defaultHealthPoint)
        {
            Debug.Log("Player already at max health");
        }
    }

    protected virtual void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(
                explosionPrefab,
                transform.position,
                transform.rotation
            );

            Destroy(explosion, 1f);
        }

        onDead?.Invoke();
        Destroy(gameObject);
    }
}
