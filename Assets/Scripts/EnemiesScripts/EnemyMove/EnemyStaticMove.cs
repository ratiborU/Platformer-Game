using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticMove : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private Enemy enemy;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        enemy = gameObject.GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (enemy.canMove)
            enemyRb.velocity = new Vector2(0, enemyRb.velocity.y);
    }
}
