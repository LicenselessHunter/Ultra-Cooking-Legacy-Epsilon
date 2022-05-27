using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveLowerHalf : MonoBehaviour
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
        if (col.gameObject.tag != "OliveShot")
        {
            oAttack.FlyNew();
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(reEnableCollider());
        }
    }

    IEnumerator reEnableCollider()
    {
        yield return new WaitForSeconds(3.3f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
