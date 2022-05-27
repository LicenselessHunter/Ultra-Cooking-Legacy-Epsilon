using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTomatoAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float bounceX;
    public float bounceY;

    public float bounceAttackX;
    public float bounceAttackY;
    public bool startAttack;

    private bool walking = true;
    GameObject Player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (startAttack == true && walking == true)
        {
            StartCoroutine(CTomatoMainAttack());
        }

        if (walking == true && startAttack == false)
        {
            StartCoroutine(CTomatoWalk());
        }
    }

    IEnumerator CTomatoWalk()
    {
        walking = false;
        anim.SetBool("Attacking", false);

        var playerPosX = Player.transform.position.x;

        if (playerPosX < transform.position.x)
        {
            rb.velocity = new Vector2(bounceX, bounceY);
        }

        else if (playerPosX > transform.position.x)
        {
            rb.velocity = new Vector2(-bounceX, bounceY);
        }

        yield return new WaitForSeconds(1.5f);
        walking = true;
    }

    IEnumerator CTomatoMainAttack()
    {
        startAttack = false;
        walking = false;

        anim.SetBool("Attacking", true);

        var playerPosX = Player.transform.position.x;

        if (playerPosX < transform.position.x)
        {
            rb.velocity = new Vector2(bounceAttackX, bounceAttackY);
        }

        else if (playerPosX > transform.position.x)
        {
            rb.velocity = new Vector2(-bounceAttackX, bounceAttackY);
        }

        yield return new WaitForSeconds(2f);
        walking = true;

    }

}
