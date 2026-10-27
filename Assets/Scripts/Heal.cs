using UnityEngine;

public class Heal : MonoBehaviour
{
    public Enemyhealth health;
    public int healAmount;
    public float flySpeed;
    public float direction;
    [HideInInspector] public float angle;

    private void Start()
    {
        angle = (1 - direction) * 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.HealPlayer(healAmount);
            health.TakeDamage(1000);
        }
    }

    private void Update()
    {
        var newPosition = transform.position;
        newPosition.y += direction * Time.deltaTime * flySpeed;
        transform.position = newPosition;
    }
}
