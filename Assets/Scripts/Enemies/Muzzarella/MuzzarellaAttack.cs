using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzarellaAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    GameObject Player;
    Animator anim;

    private Vector2 actualPos;
    Vector2 muzzarellaScale;

    private bool canShoot = true;
    public float timeBetweenAttacks = 0.7f;
    public float jumpSpeed;
    private int availableShots = 3;
    private int availableBusrts = 3;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        muzzarellaScale = gameObject.transform.localScale;
    }

    void FixedUpdate()
    {
        if (canShoot == true)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }

        var playerPosX = Player.transform.position.x; //Aqui se consulta la posici�n x del jugador. 

        if (transform.position.x > playerPosX)
        {
            muzzarellaScale.x = Mathf.Abs(gameObject.transform.localScale.x);
            transform.localScale = muzzarellaScale;
        }
        else if (transform.position.x < playerPosX)
        {
            muzzarellaScale.x = -Mathf.Abs(gameObject.transform.localScale.x);
            transform.localScale = muzzarellaScale;
        }

    }

    IEnumerator Shoot()
    {
        if (availableShots == 0 && availableBusrts != 0) //Esto es para recargar las rafagas
        {
            availableBusrts -= 1;
            anim.SetBool("Chewing", true);
            yield return new WaitForSeconds(timeBetweenAttacks);
            anim.SetBool("Chewing", false);
            availableShots = 3;
            canShoot = true;
        }

        else if (availableBusrts == 0)  //Aqui es cuando salta y repite las rafagas.
        {
            anim.SetBool("Idle", true);
            rb.velocity = new Vector2(0, jumpSpeed);
            yield return new WaitForSeconds(1.5f);
            availableBusrts = 3;
            canShoot = true;
        }

        else if (availableShots != 0) //Esto es para disparar cada una de las 3 balas de una rafaga
        {
            availableShots -= 1;
            actualPos = transform.position;
            anim.SetBool("Attacking", true);
            anim.SetBool("Idle", false);
            GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectiles/MuzzarellaShot") as GameObject);

            var playerPosX = Player.transform.position.x; //Aqui se consulta la posici�n x del jugador. 

            if (transform.position.x > playerPosX)
            {
                projectile.transform.position = new Vector2(actualPos.x - 0.6f, actualPos.y);
            }
            else if (transform.position.x < playerPosX)
            {
                projectile.transform.position = new Vector2(actualPos.x + 0.6f, actualPos.y);
            }

            yield return new WaitForSeconds(0.2f);

            anim.SetBool("Attacking", false);
            canShoot = true;
        }
    }
}
