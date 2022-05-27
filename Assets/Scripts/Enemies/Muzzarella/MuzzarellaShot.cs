using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzarellaShot : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rb;
    GameObject Player;

    // Start is called before the first frame update
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
            projectileSpeed = -Mathf.Abs(projectileSpeed);
        }
        else if (transform.position.x < playerPosX)
        {
            projectileSpeed = Mathf.Abs(projectileSpeed);
        }
    }

    void Update()
    {
        rb.velocity = new Vector2(projectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6) //Si balas impactan contra algo de layer "ground".
        {
            Destroy(gameObject);
        }
    }
}
