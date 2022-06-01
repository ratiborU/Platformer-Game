using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRL : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy enemy;
    public float speed;
    private int direction = 1;
    private Transform groundDetection;
    private Transform wallDetection;
    public LayerMask Ground;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemy = gameObject.GetComponent<Enemy>();
        groundDetection = transform.Find("GroundDetection").transform;
        wallDetection = transform.Find("WallDetection").transform;
    }

    void FixedUpdate()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (enemy.canMove)
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 0.3f, Ground);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, Ground);
        if (!groundInfo.collider || wallInfo.collider)
            Flip();
    }    

    void Flip()
    {
        transform.eulerAngles = new Vector3(0, 90 + direction * 90, 0);
        direction *= -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Flip();
    }
}
