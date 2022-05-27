using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject[] roomColliders;
    public bool defeatedBoss;
    PlayerController pc;
    public GameObject exitDoor;
    public GameObject boss;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        boss.SetActive(false);
        exitDoor.SetActive(false);

        foreach (var item in roomColliders)
        {
            item.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if(defeatedBoss)
        {
            //Enable the exit door
            exitDoor.SetActive(true);

            //Disable the colliders, so the player can go farm the remaining ingredients
            foreach (var item in roomColliders)
            {
                item.SetActive(false);
            }
        }
    }

    IEnumerator activateBossRoom()
    {
        //Wait until the player is inside to enable the room locks
        yield return new WaitForSeconds(0.5f);
        foreach (var item in roomColliders)
        {
            item.SetActive(true);
        }
        boss.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            StartCoroutine(activateBossRoom());
        }
    }
}
