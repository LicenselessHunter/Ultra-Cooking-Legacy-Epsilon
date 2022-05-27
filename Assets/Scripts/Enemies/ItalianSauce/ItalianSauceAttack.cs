using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItalianSauceAttack : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject Player;
    Animator anim;

    private bool canShoot = true;
    private Vector2 actualPos;
    private int shootBursts = 2;
    public float speedX;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canShoot == true)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (shootBursts == 0)
        {
            anim.SetBool("Idle", true);
            var playerPosX = Player.transform.position.x; //Aqui se consulta la posici�n x del jugador. 

            if (transform.position.x > playerPosX)
            {
                rb.velocity = new Vector2(-Mathf.Abs(speedX), 0);

            }
            if (transform.position.x < playerPosX)
            {
                rb.velocity = new Vector2(Mathf.Abs(speedX), 0);
               
            }
            yield return new WaitForSeconds(1.5f);
            shootBursts = 2;
            canShoot = true;
        }

        else if(shootBursts != 0)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("WaitAttack", true);
            
            yield return new WaitForSeconds(0.6f);

            anim.SetBool("WaitAttack", false);
            anim.SetBool("Attack", true);
            
            actualPos = transform.position;
            GameObject projectile1 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_0") as GameObject);
            GameObject projectile2 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_1") as GameObject);
            GameObject projectile3 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_2") as GameObject);
            GameObject projectile4 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_3") as GameObject);

            projectile1.transform.position = new Vector2(actualPos.x, actualPos.y);
            projectile2.transform.position = new Vector2(actualPos.x, actualPos.y);
            projectile3.transform.position = new Vector2(actualPos.x, actualPos.y);
            projectile4.transform.position = new Vector2(actualPos.x, actualPos.y);

            yield return new WaitForSeconds(1f);
            
            anim.SetBool("Attack", false);
            canShoot = true;
            shootBursts -= 1;
        }
    }
}
