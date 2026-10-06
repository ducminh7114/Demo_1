using UnityEngine;

public class playerShooting : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public float shootingInterval;
    private float lastBulletTime;
    public Vector3 bulletOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Hold to shoot
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Button Pressed");
            UpdateFiring();
        }    
    }

    private void UpdateFiring()
    {
        if (Time.time - lastBulletTime > shootingInterval)
        {
            ShootBullet();
            lastBulletTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefabs, transform.position + bulletOffset, transform.rotation);

    }
}
