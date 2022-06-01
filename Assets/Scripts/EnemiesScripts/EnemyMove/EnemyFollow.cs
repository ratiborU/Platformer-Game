using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform playerTr;
    private Enemy enemy;
    public Animator animator;

    private Rigidbody2D enemyRb;
    public float speed;
    public float followSpeed;
    private int direction = 1;
    private Transform groundDetection;
    private Transform wallDetection;
    public LayerMask Ground;

    private float distanceToSeePlayer = 8f;
    private bool isFoundPlayer;
    private int directionToPlayer;

    private RaycastHit2D wallInfo;
    private RaycastHit2D groundInfo;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        groundDetection = transform.Find("GroundDetection").transform;
        wallDetection = transform.Find("WallDetection").transform;
    }

    void FixedUpdate()
    {
        SetDirectionToPlayer();
        EnemyMove();
    }

    void Update()
    {
        
    }

    void SetDirectionToPlayer()
    {
        if (playerTr.position.x - transform.position.x > 0)
            directionToPlayer = 1;
        else if (playerTr.position.x - transform.position.x < 0)
            directionToPlayer = -1;
        isFoundPlayer = Vector2.Distance(playerTr.position, transform.position) > distanceToSeePlayer;
        animator.SetBool("IsFound", !isFoundPlayer);
    }

    void EnemyMove()
    {
        if (enemy.canMove)
        {
            if (isFoundPlayer)
            {
                enemyRb.velocity = new Vector2(direction * speed, enemyRb.velocity.y);
                wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 0.3f, Ground);
                groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, Ground);
                if (!groundInfo.collider || wallInfo.collider)
                {
                    transform.eulerAngles = new Vector3(0, 90 + direction * 90, 0);
                    direction *= -1;
                }
            }
            else
            {
                direction = directionToPlayer;
                enemyRb.velocity = new Vector2(directionToPlayer * followSpeed, enemyRb.velocity.y);
                transform.eulerAngles = new Vector3(0, 90 - direction * 90, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            transform.eulerAngles = new Vector3(0, 90 + direction * 90, 0);
            direction *= -1;
        }
    }
}
