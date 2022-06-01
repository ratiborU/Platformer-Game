using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoom : MonoBehaviour
{
    private Transform playerTr;
    private Rigidbody2D enemyRb;
    private Enemy enemy;
    private bool isStartToBoom;
    public float boomTime = 1f;
    private float distance;

    public GameObject boomObject;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        
        SetDistance();
        CheckPlayer();
        Boom();
    }

    void CheckPlayer()
    {
        if (distance < 3f)
        {
            enemy.canMove = false;
            enemyRb.velocity = new Vector2(0, enemyRb.velocity.y);
            isStartToBoom = true;
        }
    }

    void Boom()
    {
        if (isStartToBoom)
        {
            if (boomTime <= 0)
            {
                Instantiate(boomObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                boomTime -= Time.deltaTime;
            }
        }

    }

    void SetDistance()
    {
        distance = Vector2.Distance(playerTr.position, transform.position);
    }
}
