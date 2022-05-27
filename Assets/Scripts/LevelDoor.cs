using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "SelectorDoor")
            {
                SceneManager.LoadScene(3);
            }
            

            if (gameObject.name == "PuertaPencaCasa")
            {
                DBmanager.completeQuantity++;
                SceneManager.LoadScene(2);
            }

        }        
    }
}
