using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveWeakness : MonoBehaviour
{
    private OliveAttack oAttack;
    GameObject Olive;

    void Start()
    {
        Olive = transform.parent.gameObject;
        oAttack = Olive.GetComponent<OliveAttack>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject OliveOil = Instantiate(Resources.Load("Prefabs/Drops/DropOlive") as GameObject);
            GameObject OliveExplosion = Instantiate(Resources.Load("Prefabs/EnemyExplosions/OliveDeath") as GameObject);

            OliveExplosion.transform.localPosition = new Vector2(Olive.transform.position.x, Olive.transform.position.y);
            OliveOil.transform.localPosition = new Vector2(Olive.transform.position.x, Olive.transform.position.y);

            Destroy(Olive);
        }
    }
}
