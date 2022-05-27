using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItalianSauceProjectile : MonoBehaviour
{
    public float projectileSpeedX;
    public float projectileSpeedY;
    private Rigidbody2D rb;
    GameObject Player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        var playerPosX = Player.transform.position.x; //Aqui se consulta la posici�n x del jugador. 

        if (transform.position.x > playerPosX)
        {
            rb.velocity = new Vector2(-Mathf.Abs(projectileSpeedX), projectileSpeedY);
        }
        else if (transform.position.x < playerPosX)
        {
            rb.velocity = new Vector2(Mathf.Abs(projectileSpeedX), projectileSpeedY);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6) //Si balas impactan contra algo de layer "ground".
        {
            Destroy(gameObject);
        }
    }
}
