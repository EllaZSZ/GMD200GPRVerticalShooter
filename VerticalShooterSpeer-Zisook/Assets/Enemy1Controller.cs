using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnspeed;
    [SerializeField] private float evadedist;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int BULLET_DELAY;
    [SerializeField] private int spawndistance;
    [SerializeField] private float health;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int willsummon;
    private GameObject healthBar;
    private int bulletDelay;
    private bool evading;
    private GameObject player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player/Ship");
        healthBar = transform.parent.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angletoplayer = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        if (Vector3.Distance(transform.position, player.transform.position) < evadedist) 
        {
            angletoplayer += 180;
            evading = true;
        } else
        {
            evading = false;
        }
        float angledif = (angletoplayer - transform.eulerAngles.z - 90) % 360;
        angledif = angledif > 180 ? angledif - 360 : angledif < -180 ? angledif + 360 : angledif;
        if (Math.Abs(angledif) < turnspeed)
        {
            transform.Rotate(0, 0, angledif);
            if (bulletDelay > BULLET_DELAY && evading == false)
            {
                Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.velocity = transform.up * bulletSpeed;
                Destroy(bullet.gameObject, 1);
                bulletDelay = 0;
            }
        } else
        {
            transform.Rotate(0, 0, turnspeed * Mathf.Sign(angledif));
        }
        rb.AddForce(transform.up * speed);
        bulletDelay++;
    }
    void Update()
    {
        //healthbar
        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, 0);
        healthBar.transform.localScale = new Vector3(health * 0.5f, 0.05f, 1);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("playerbullet"))
        {
            Destroy(other.gameObject);
            health -= 0.201f;
            if (health <= 0)
            {
                if (transform.parent.parent.childCount == 1)
                {
                    float angle = Random.value * 2 * Mathf.PI;

                    for (int i = 0; i < willsummon; i++)
                    {
                        GameObject yetAnotherEnemy = Instantiate(enemyPrefab, new Vector3(player.transform.position.x + Mathf.Cos(angle + i * 10) * spawndistance, player.transform.position.y + Mathf.Sin(angle + i * 10) * spawndistance, 0), Quaternion.identity);
                        yetAnotherEnemy.transform.GetChild(1).gameObject.GetComponent<Enemy1Controller>().willsummon++;
                        yetAnotherEnemy.transform.GetChild(1).gameObject.GetComponent<Enemy1Controller>().health = 1;
                        yetAnotherEnemy.transform.parent = transform.parent.parent;
                    }
                    PlayerController.health += 0.5f;
                }
                Destroy(transform.parent.gameObject);
                ScoreController.score += 10;
            }
        }
    }
}
