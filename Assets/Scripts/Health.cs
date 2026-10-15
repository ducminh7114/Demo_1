using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint;
    public System.Action onDead;
    public System.Action onHealthChanged;
    [HideInInspector] public int healthPoint;

    private void Start()
    {
        healthPoint = defaultHealthPoint;
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if ( healthPoint <= 0)
        {
            return;
        }

        healthPoint -= damage;
        onHealthChanged?.Invoke();
        if ( healthPoint <= 0 )
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1);
        Destroy(gameObject);
        onDead?.Invoke();
    }
}
