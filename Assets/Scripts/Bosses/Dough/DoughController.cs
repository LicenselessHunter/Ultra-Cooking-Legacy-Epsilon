using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoughController : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public float maxHealth;
    public Image HPBar;
    public float damageShield;

    [Header("Components")]
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;

    
    [Header("Attack / Progression")]
    public BossRoom bossRoom;
    public Transform enemySpawn1;
    public Transform enemySpawn2;
    public float attackCounter;
    public GameObject cherryTomatoPrefab;

    void Start()
    {
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        damageShield -= 1 * Time.deltaTime;
        attackCounter -= 1 * Time.deltaTime;
        HPBar.fillAmount = health / maxHealth;

        if(health <= 0)
        {
            bossRoom.defeatedBoss = true;
            Destroy(gameObject);
        }

        if(attackCounter <= 0)
        {
            chooseAttack();
        }
    }

    public void hurtBoss(int damage)
    {
        if(damageShield <= 0)
        {
            health -= damage;
            StartCoroutine(showDamage());
            damageShield = 2.5f;
        }
    }

    void chooseAttack()
    {
        int result = Random.Range(1,4);
        if(result == 1)
        {
            Debug.Log("Jump");
            StartCoroutine(jumpAttack());
            attackCounter = 5;
        }
        else if(result == 2)
        {
            Debug.Log("Shoot");
            summonEnemies();
            attackCounter = 5;
        }
        else if(result == 3)
        {
            Debug.Log("Shoot");
            StartCoroutine(shoot());
            attackCounter = 5;
        }
    }

    IEnumerator showDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
    }

    IEnumerator jumpAttack()
    {
        anim.SetBool("Jumping", true);
        rb.velocity = new Vector2(0, 10);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Jumping", false);
    }

    void summonEnemies()
    {
        Instantiate(cherryTomatoPrefab, enemySpawn1.position, enemySpawn1.rotation);
        Instantiate(cherryTomatoPrefab, enemySpawn2.position, enemySpawn2.rotation);
    }

    IEnumerator shoot()
    {
        anim.SetBool("Attacking", true);
        GameObject projectile1 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_0") as GameObject);
        GameObject projectile2 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_1") as GameObject);
        GameObject projectile3 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_2") as GameObject);
        GameObject projectile4 = Instantiate(Resources.Load("Prefabs/Projectiles/ItalianSauceShot_3") as GameObject);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Attacking", false);

        projectile1.transform.position = new Vector2(transform.position.x, transform.position.y);
        projectile2.transform.position = new Vector2(transform.position.x, transform.position.y);
        projectile3.transform.position = new Vector2(transform.position.x, transform.position.y);
        projectile4.transform.position = new Vector2(transform.position.x, transform.position.y);
    }
}