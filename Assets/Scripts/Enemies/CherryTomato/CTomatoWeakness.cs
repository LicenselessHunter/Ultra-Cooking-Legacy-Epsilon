using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTomatoWeakness : MonoBehaviour
{
    GameObject CherryTomato;
    public Transform CherryTomatoDrop;

    void Start()
    {
        CherryTomato = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject CherryTomatoDrop = Instantiate(Resources.Load("Prefabs/Drops/DropTomato") as GameObject);
            GameObject TomatoExplosion = Instantiate(Resources.Load("Prefabs/EnemyExplosions/CherryTomatoDeath") as GameObject);

            TomatoExplosion.transform.localPosition = new Vector2(CherryTomato.transform.position.x, CherryTomato.transform.position.y);
            CherryTomatoDrop.transform.localPosition = new Vector2(CherryTomato.transform.position.x, CherryTomato.transform.position.y);

            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * 10;

            Destroy(CherryTomato);
        }
    }
}
