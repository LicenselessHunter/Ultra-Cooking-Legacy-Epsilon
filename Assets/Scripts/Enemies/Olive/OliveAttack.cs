using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveAttack : MonoBehaviour
{
    GameObject Player;
    public float speedX = 3;
    public float speedY = 0;
    private Rigidbody2D rb;

    private Vector2 actualPos;
    private int availableShots = 5;

    private bool canShoot = true;
    Animator anim;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (availableShots != 0 && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }

        var playerPosX = Player.transform.position.x; //Aqui se consulta la posici�n x del jugador. 

        rb.velocity = new Vector2(speedX, speedY);

        if (transform.position.x > playerPosX)
        {
            speedX = -Mathf.Abs(speedX);
        }
        else if(transform.position.x < playerPosX)
        {
            speedX = Mathf.Abs(speedX);
        }

        if (transform.position.x < playerPosX + 0.1f && transform.position.x > playerPosX - 0.1f && speedX != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            
            if (availableShots == 0)
            {
                speedX = 0;
                speedY = -9;
            }
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //ESTO ESTA AQUI POR ALGUNA RAZ�N PORQUE EL FREEZE DE ROTACI�N SE QUITA
        }
    }

    public void FlyNew()
    {
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        anim.SetBool("Resting", true);
        yield return new WaitForSeconds(1.4f);
        speedY = 3;
        anim.SetBool("Resting", false);
        yield return new WaitForSeconds(2f);
        speedY = 0;
        speedX = 3;
        availableShots = 5;
    }

    IEnumerator Shoot()
    {
        availableShots -= 1;
        actualPos = transform.position;
        GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectiles/OliveShot") as GameObject);
        projectile.transform.position = new Vector2(actualPos.x, actualPos.y);
        anim.SetBool("Shooting", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Shooting", false);
        canShoot = true;
    }
}