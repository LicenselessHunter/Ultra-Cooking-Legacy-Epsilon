using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItalianSauceWeakness : MonoBehaviour
{
    private ItalianSauceAttack ISAttack;
    GameObject ItalianSauce;

    void Start()
    {
        ItalianSauce = transform.parent.gameObject;
        ISAttack = ItalianSauce.GetComponent<ItalianSauceAttack>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject SAUCE = Instantiate(Resources.Load("Prefabs/Drops/DropItalianSauce") as GameObject);
            GameObject ITALIANSAUCEExplosion = Instantiate(Resources.Load("Prefabs/EnemyExplosions/ItalianSauceDeath") as GameObject);

            ITALIANSAUCEExplosion.transform.localPosition = new Vector2(ItalianSauce.transform.position.x, ItalianSauce.transform.position.y);
            SAUCE.transform.localPosition = new Vector2(ItalianSauce.transform.position.x, ItalianSauce.transform.position.y);

            Destroy(ItalianSauce);
        }
    }
}
