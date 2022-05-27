using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public int playerPushForce;
    public int damageToDeal;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PlayerController pc = col.GetComponent<PlayerController>();
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            pc.hurtPlayer(damageToDeal);
            rb.velocity = Vector2.up * playerPushForce;
        }
        if(col.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
    }
}