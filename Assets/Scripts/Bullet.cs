using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed;
    public int damage;
    public float direction;
    public float Timer = 0f;
    public float angle;

    void Start()
    {
        angle = (1 - direction) * 90f;
        Destroy(gameObject, Timer);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemyhealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y += direction * Time.deltaTime * flySpeed;
        transform.position = newPosition;
    }
}
