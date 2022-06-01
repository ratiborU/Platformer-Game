using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private Enemy enemy;
    private Transform playerTr;
    public float speed;
    private Vector2 directionToPlayer;
    private Vector2 ordinaryMoving;
    private float time;
    private int direction;
    private float distance;
    private bool follow;
    private float SeeDistance = 12f;
    private float LoseDistance = 20f;
    public float looping = 1.5f;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        EnemyMove();
        SetDirection();
        SetDistance();
        SetFollow();
    }

    void Update()
    {
    }

    void EnemyMove()
    {
        if (enemy.canMove)
        {
            time += Time.deltaTime;
            ordinaryMoving = new Vector2((float)Math.Cos(time * 3) * looping, (float)Math.Sin(time * 3) * looping);
            SetDirectionToPlayer();

            if (follow)
                enemyRb.velocity = directionToPlayer;
            else
                enemyRb.velocity = ordinaryMoving;
        }
    }

    void SetDirection()
    {
        if (directionToPlayer.x > 0)
            direction = 1;
        else if (directionToPlayer.x < 0)
            direction = -1;
        transform.eulerAngles = new Vector3(0, 90 - direction * 90, 0);
    }

    void SetDistance()
    {
        distance = Vector2.Distance(playerTr.position, transform.position);
    }

    void SetDirectionToPlayer()
    {
        directionToPlayer = new Vector2(playerTr.position.x - transform.position.x, playerTr.position.y - transform.position.y);
        directionToPlayer += ordinaryMoving;
        directionToPlayer.Normalize();
        directionToPlayer *= speed;
    }

    void SetFollow()
    {
        if (distance < SeeDistance)
            follow = true;
        else if (distance > LoseDistance)
            follow = false;
    }
}
