using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayerPenca : MonoBehaviour
{

    [Header("Components")]
    private Rigidbody2D rb;


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



    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

    
        
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
    }

    private void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, GroundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
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

}
