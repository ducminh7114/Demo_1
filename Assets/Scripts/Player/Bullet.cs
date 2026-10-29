using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed;
    public int damage;
    public float direction;
    public float lifeTime = 0f;

    private Vector2 moveDirection;


    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
        transform.up = moveDirection;
    }
    void Start()
    {
       Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemyhealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(moveDirection * flySpeed * Time.deltaTime);
    }

}