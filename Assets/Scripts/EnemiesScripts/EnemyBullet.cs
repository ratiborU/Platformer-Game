using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Transform playerTr;
    private Rigidbody2D bulletRb;
    private Vector2 direction;
    private Vector2 target;
    public float speed = 6;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();

        BulletMove();
    }

    void Update()
    {
    }

    void BulletMove()
    {
        target = new Vector2(playerTr.position.x - transform.position.x, playerTr.position.y - transform.position.y);
        target.Normalize();
        target *= speed;
        bulletRb.velocity = target;
        Destroy(gameObject, 7);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
