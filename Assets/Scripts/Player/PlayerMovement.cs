using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerAttack pa;
    private PlayerController pc;

    [Header("Movement")]
    public float speed;
    private float moveInput;
    //bool crouching;

    [Header("Jump")]
    public float jumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask GroundLayer;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Dash")]
    public Image staminaBar;
    public bool canDash = true;
    public float dashingTime;
    public float dashSpeed;
    public float dashJumpIncrease;
    public float timeBtwDashes;
    public float dashCooldown = 0;

    [Header("Dropdown")]
    public bool dropping;
    public bool canMove = true;
    float dropSpeed = 10;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        pa = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(canMove)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    private void Update()
    {
        dashCooldown -= Time.deltaTime;

        staminaBar.fillAmount = dashCooldown / 2;

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, GroundLayer);
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            DashAbility();
            if(moveInput > 0)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            else if (moveInput < 0)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            else
            {
                if(transform.rotation.y < 0)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
            dashCooldown = 2;
        }

        if(Input.GetKeyDown(KeyCode.S) && !isGrounded && !dropping)
        {
            StartCoroutine(Dropdown());
        }

        if(moveInput != 0)
        {
            anim.SetBool("Running", true);

            if(moveInput > 0)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (moveInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(!isGrounded)
        {
            anim.SetBool("Jumping", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void DashAbility()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dropdown()
    {
        pc.canBeDamaged = false;
        anim.SetBool("Dropping", true);
        dropping = true;
        canMove = false;
        rb.velocity = Vector2.down * dropSpeed;
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Dropping", false);
        dropping = false;
        canMove = true;
        pc.canBeDamaged = true;
    }

    IEnumerator Dash()
    {
        anim.SetBool("Dashing", true);
        GameObject dashParticles = Instantiate(Resources.Load("Prefabs/Player/DashParticles") as GameObject, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.eulerAngles.y-90, transform.rotation.z));
        pc.canBeDamaged = false;
        dashParticles.transform.SetParent(transform);
        canDash = false;
        speed = dashSpeed;
        jumpForce = dashJumpIncrease;
        yield return new WaitForSeconds(dashingTime);
        pa.killEnemies();
        speed = 7;
        jumpForce = 10;
        yield return new WaitForSeconds(timeBtwDashes);
        anim.SetBool("Dashing", false);
        pc.canBeDamaged = true;
        canDash = true;
    }
}