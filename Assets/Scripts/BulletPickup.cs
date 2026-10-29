
using UnityEngine;
using static playerShooting;

public class BulletPickup : MonoBehaviour
{
    public BulletType bulletTypeToGive = BulletType.Spread;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerShooting player =
            collision.GetComponent<playerShooting>();

        if (player != null)
        {
            player.SetBulletType(bulletTypeToGive);
            Destroy(gameObject);
        }
    }
}

