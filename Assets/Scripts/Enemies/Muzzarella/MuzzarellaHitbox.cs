using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzarellaHitbox : MonoBehaviour
{
    GameObject Muzzarella;
    public Transform MuzzarellaDrop;

    void Start()
    {
        Muzzarella = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerMovement pm = col.GetComponent<PlayerMovement>();
            if(pm.dropping)
            {
                GameObject MuzzarellaDrop = Instantiate(Resources.Load("Prefabs/Drops/DropMuzzarella") as GameObject, Muzzarella.transform.position, Muzzarella.transform.rotation);
                GameObject MuzzarellaExplosion = Instantiate(Resources.Load("Prefabs/EnemyExplosions/MuzzarellaDeath") as GameObject, Muzzarella.transform.position, Muzzarella.transform.rotation);
                GameObject MuzzarellaParent = transform.parent.gameObject;
                Destroy(MuzzarellaParent);
            }
            else
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(Random.Range(2,10), 10);
                col.GetComponent<PlayerController>().hurtPlayer(5);
            }
        }
    }
}