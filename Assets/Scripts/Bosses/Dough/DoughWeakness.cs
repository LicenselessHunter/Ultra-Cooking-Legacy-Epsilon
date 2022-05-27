using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughWeakness : MonoBehaviour
{
    DoughController dc;

    void Start()
    {
        dc = transform.parent.GetComponent<DoughController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("Hurt boss");
            dc.hurtBoss(15);
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(5,20);
        }
        Debug.Log("Entered area");
    }
}