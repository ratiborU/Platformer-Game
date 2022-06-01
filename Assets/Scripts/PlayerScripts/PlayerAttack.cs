using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMove playerMove;
    private Rigidbody2D playerRb;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private bool isAttackingEnemy;

    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemies;
    public int damage;
    private Vector2 directionOfForce;

    public GameObject effect;
    public Animator swordAnimator;
    public Animator animator;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerMove = gameObject.GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        DoPlayerAttack();
    }

    void Update()
    {
        
    }

    void DoPlayerAttack()
    {
        if (timeBtwAttack <= 0)
        {
            isAttackingEnemy = false;
            if (Input.GetKey(KeyCode.J))
            {
                swordAnimator.SetBool("IsAttacking", true);
                animator.SetBool("IsAttacking", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                if (enemiesToDamage.Length != 0)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);
                    isAttackingEnemy = true;
                }
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            if (isAttackingEnemy)
                AttackRecoil();
            timeBtwAttack -= Time.deltaTime;
            swordAnimator.SetBool("IsAttacking", false);
            animator.SetBool("IsAttacking", false);
        }
    }

    void AttackRecoil()
    {
        if (timeBtwAttack - 0.37 > 0)
        {
            playerMove.isAttacking = true;
            playerRb.velocity = new Vector2(-playerMove.direction * timeBtwAttack * timeBtwAttack * 40, playerRb.velocity.y);
        }
        else
        {
            playerMove.isAttacking = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}