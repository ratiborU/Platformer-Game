using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBoss : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Bee;
    public GameObject BeeShoot;
    private Transform playerTr;
    public EnemyFly enemyFly;

    private float timeBtwAttacks;
    public float startTimeBtwAttacks;
    private float timeBtwShoots;
    public float startTimeBtwShoots;

    private bool isPlayerDetected = false;
    private float distance;
    public int attackVariant;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwAttacks = startTimeBtwAttacks;
        timeBtwShoots = startTimeBtwShoots;
        enemyFly.looping = 4f;

    }

    void FixedUpdate()
    {
        SetIsPlayerDetected();
        if (isPlayerDetected) 
            Attacks();
    }

    void Attacks()
    {
        if (timeBtwAttacks <= 0)
        {
            enemyFly.speed = 2f;
            var rnd = new System.Random();
            attackVariant = rnd.Next(3);
            if (attackVariant == 0)
                Attack0();
            if (attackVariant == 1)
            {
                Attack1(3, 0);
                Attack1(-3, 0);
                Attack1(0, 3);
            }
            if (attackVariant == 2)
                Attack2();
            timeBtwAttacks = startTimeBtwAttacks;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    void Attack0()
    {
        enemyFly.speed = 6.5f;
    }

    void Attack1(float x, float y)
    {
        Instantiate(Bee, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), Quaternion.identity);
    }

    void Attack2()
    {
        Instantiate(BeeShoot, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
    }

    void SetIsPlayerDetected()
    {
        distance = Vector2.Distance(playerTr.position, transform.position);
        if (distance <= 12)
            isPlayerDetected = true;
    }
}
