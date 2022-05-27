using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveProjectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0, projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6) //Si balas impactan contra algo de layer "ground".
        {
            Destroy(gameObject);
        }
    }
}