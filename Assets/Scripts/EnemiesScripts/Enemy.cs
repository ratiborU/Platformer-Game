using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerMove playerMove;
    private Rigidbody2D enemyRb;
    private PlayerHealth playerHealth;

    public int health;
    private bool isAttacking;
    private float timeRecoil;
    public float startTimeRecoil;
    public bool canMove = true;
    private bool isTriggeredWithPlayer;

    public GameObject effect;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        timeRecoil = startTimeRecoil;
    }

    void FixedUpdate()
    {
        CheckDeath();
        DoDamageToPlayer();
        AttackRecoil();
    }

    void CheckDeath()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        health -= damage;
        isAttacking = true;
        Debug.Log($"hp is: {health}");
    }

    void DoDamageToPlayer()
    {
        if (isTriggeredWithPlayer)
            playerHealth.TakeDamage(1);
    }

    void AttackRecoil()
    {
        if (isAttacking)
        {
            if (canMove)
                enemyRb.velocity = new Vector2(playerMove.direction * 4, 4);
            canMove = false;
            timeRecoil -= Time.deltaTime;
            if (timeRecoil < 0)
            {
                isAttacking = false;
                timeRecoil = startTimeRecoil;
                canMove = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTriggeredWithPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTriggeredWithPlayer = false;
    }
}
