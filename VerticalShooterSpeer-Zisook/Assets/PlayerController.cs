using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnspeed;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int BULLET_DELAY;
    private int bulletDelay;
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.AddForce(transform.up * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.AddForce(-transform.up * speed / 3);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.AddTorque(-turnspeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.AddTorque(turnspeed);
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (bulletDelay > BULLET_DELAY)
            {
                Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.velocity = transform.up * bulletSpeed;
                Destroy(bullet.gameObject, 2.0f);
                bulletDelay = 0;
            }
        }
        bulletDelay++;
    }

}
