using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootung : MonoBehaviour
{
    public GameObject bullet;
    private Transform playerTr;
    private float timeBtwShoot;
    public float startTimeBtwShoot;
    private bool isPlayerDetected;
    private float distance;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShoot = startTimeBtwShoot;
    }

    void FixedUpdate()
    {
        SetIsPlayerDetected();
        SetDistance();
        Shooting();
    }

    void Shooting()
    {
        if (isPlayerDetected)
        {
            if (timeBtwShoot <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                timeBtwShoot = startTimeBtwShoot;
            }
            else
            {
                timeBtwShoot -= Time.deltaTime;
            }
        }
    }

    void SetIsPlayerDetected()
    {
        if (distance <= 8)
            isPlayerDetected = true;
        else
            isPlayerDetected = false;
    }

    void SetDistance()
    {
        distance = Vector2.Distance(playerTr.position, transform.position);
    }
}
