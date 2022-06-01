using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomScript : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform playerTr;
    public float distance;
    public float boomRadius;
    public GameObject boomEffect;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        SetDistance();
        Boom();
    }

    void Update()
    {
        
    }

    void Boom()
    {
        if (distance <= boomRadius)
            playerHealth.TakeDamage(1);
        Instantiate(boomEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void SetDistance()
    {
        distance = Vector2.Distance(playerTr.position, transform.position);
    }
}
