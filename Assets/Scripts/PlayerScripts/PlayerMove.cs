using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    public Animator animator;
    public float moveSpeed;
    public int direction;
    private bool facingRight = true;

    public float jumpForce;
    private bool jumpControl;
    public int doubleJumpsCount;
    public int maxJumpsCount;
    private float jumpTimeCounter;
    public float jumpTime;

    private bool onGround;
    private bool onCelling;
    private Transform GroundCheck;
    private Transform CellingCheck;
    private float checkRadius = 0.2f;
    public LayerMask Ground;

    public float dashSpeed = 40f;
    private float dashTime;
    public float startDashTime;
    private bool isDashing;
    private bool isDashed;
    public bool canDashing;

    public bool isAttacking;
    private PlayerHealth playerHealth;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        GroundCheck = gameObject.transform.Find("GroundCheck").transform;
        CellingCheck = gameObject.transform.Find("CellingCheck").transform;
        dashTime = startDashTime;
        doubleJumpsCount = maxJumpsCount - 1;
    }

    void FixedUpdate()
    {
        Walk();
        CheckGround();
    }
    
    void Update()
    {
        Jump();
        Dash();
    }

    void Walk()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (!isDashing && !isAttacking)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            SetDirection();
            if (facingRight == false && moveInput > 0)
                Flip();
            else if (facingRight == true && moveInput < 0)
                Flip();
        }
        animator.SetBool("IsRunning", moveInput != 0);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void SetDirection()
    {
        if (moveInput > 0) direction = 1;
        else if (moveInput < 0) direction = -1;
    }

    void Jump()
    {
        animator.SetFloat("FallSpeed", rb.velocity.y);
        animator.SetBool("OnGround", onGround);
        //прыжок с земли
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            jumpControl = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //прыжок в воздухе
        if (!onGround && Input.GetKey(KeyCode.Space) && !jumpControl && (doubleJumpsCount > 0))
        {
            doubleJumpsCount--;
            jumpControl = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        // обновление счетчика прыжков
        if (onGround)
        {
            doubleJumpsCount = maxJumpsCount - 1;
        }

        //контролируемый прыжок
        if (Input.GetKey(KeyCode.Space) && jumpControl && !onCelling)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        else
            jumpControl = false;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpControl = false;
        }
    }

    void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        onCelling = Physics2D.OverlapCircle(CellingCheck.position, checkRadius, Ground);
        if (onGround) isDashed = false;
    }

    public void GetDoubleJumpe()
    {
        maxJumpsCount = 2;
        doubleJumpsCount = maxJumpsCount - 1;
    }

    public void GetMoreHealth()
    {
        playerHealth.GetHealth();
    }

    public void AddHealth()
    {
        playerHealth.AddHealth();
        playerHealth.UpdateHealth();
    }

    public void GetDash()
    {
        canDashing = true;
    }

    void Dash()
    {
        animator.SetBool("IsDashing", isDashing);
        if (canDashing)
        {
            if (!isDashing && !isAttacking)
            {
                if (Input.GetKeyDown(KeyCode.E) && !isDashed)
                {
                    isDashing = true;
                }
            }
            else if (!isDashed && !isAttacking)
            {
                if (dashTime <= 0)
                {
                    isDashing = false;
                    isDashed = true;
                    dashTime = startDashTime;
                    rb.gravityScale = 1.5f;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    rb.velocity = new Vector2(direction * dashSpeed, 0f);
                    rb.gravityScale = 0;
                }
            }
        }
    }
}
