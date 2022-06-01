using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Transform playerTr;
    private PlayerMove playerMove;
    private Rigidbody2D playerRb;
    public PlayerAttack playerAttack;
    public int health;
    public int maxHealth = 2;
    private float secondsOfInvulnerability;
    public float maxSecondsOfInvulnerability;

    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float timeToRecoli;
    public float startTimeRecoil = 0.5f;
    public bool isTakingDamage;

    private Vector2 checkPointPos;


    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerMove = gameObject.GetComponent<PlayerMove>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();
        playerTr = gameObject.GetComponent<Transform>();
        health = maxHealth;
        checkPointPos = playerTr.position;
    }

    void FixedUpdate()
    {
        Death();
        SetSecondsOfInvulnerability();
        TakeDamageRecoil();
        
    }

    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    void SetSecondsOfInvulnerability()
    {
        if (secondsOfInvulnerability > 0)
            secondsOfInvulnerability -= Time.deltaTime;
        else if (secondsOfInvulnerability < 0)
            secondsOfInvulnerability = 0;
    }

    public void SetCheckPointPos(Vector2 checkPoint)
    {
        checkPointPos = checkPoint;
    }

    public void TakeDamage(int damage)
    {
        if (secondsOfInvulnerability <= 0)
        {
            isTakingDamage = true;
            timeToRecoli = startTimeRecoil;
            health -= damage;
            secondsOfInvulnerability = maxSecondsOfInvulnerability;
            UpdateHealth();
        }
    }

    void Death()
    {
        if (health <= 0)
        {
            gameObject.transform.position = checkPointPos;
            health = maxHealth;
            UpdateHealth();
        }
        
    }

    public void GetHealth()
    {
        maxHealth++;
        health = maxHealth;
    }

    public void AddHealth()
    {
        if (health != maxHealth)
            health++;
    }

    public void TakeDamageRecoil()
    {
        if (isTakingDamage)
        {
            if (timeToRecoli - 0.37 > 0)
            {
                playerMove.isAttacking = true;
                playerRb.velocity = new Vector2(-playerMove.direction * timeToRecoli * timeToRecoli * 40, 6f);
                timeToRecoli -= Time.deltaTime;
            }
            else
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerMove.isAttacking = false;
                isTakingDamage = false;
            }
        }
    }
}
