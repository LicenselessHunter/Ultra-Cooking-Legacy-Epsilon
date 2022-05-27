using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTomatoRange : MonoBehaviour
{
    private CTomatoAttack cTomatoAttackController;
    GameObject enemyCTomato;

    void Start()
    {
        enemyCTomato = transform.parent.gameObject;
        cTomatoAttackController = enemyCTomato.GetComponent<CTomatoAttack>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            cTomatoAttackController.startAttack = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(reEnableCollider());
        }
    }
    
    IEnumerator reEnableCollider()
    {
        yield return new WaitForSeconds(3.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    
}
