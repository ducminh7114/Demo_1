
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float flySpeed = 5f;
    public int damage = 10;
    public float direction = -1f;
    public float Timer = 5f;

    [HideInInspector] public float angle;

    private Vector2 moveDirection;
    private bool customDirection = false;

    void Start()
    {
        if (!customDirection)
        {
            // Original vertical movement
            moveDirection = Vector2.up * direction;
        }

        Destroy(gameObject, Timer);
    }

    public void SetDirection(Vector2 newDirection)
    {
        customDirection = true;
        moveDirection = newDirection.normalized;

        float rotationAngle =
            Mathf.Atan2(moveDirection.y, moveDirection.x)
            * Mathf.Rad2Deg - 90f;

        transform.rotation =
            Quaternion.Euler(0, 0, rotationAngle);
    }

    void Update()
    {
        transform.position +=
            (Vector3)(moveDirection * flySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health player = collision.GetComponent<Health>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
