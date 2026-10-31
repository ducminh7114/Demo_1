using UnityEngine;

public class Heal : MonoBehaviour
{
    public int healAmount;
    public float flySpeed;
    public float direction;
    private bool collected;
    [HideInInspector] public float angle;

    private void Start()
    {
        angle = (1 - direction) * 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected)
            return;

        var playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.HealPlayer(healAmount);
            collected = true;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var newPosition = transform.position;
        newPosition.y += direction * Time.deltaTime * flySpeed;
        transform.position = newPosition;
    }
}
