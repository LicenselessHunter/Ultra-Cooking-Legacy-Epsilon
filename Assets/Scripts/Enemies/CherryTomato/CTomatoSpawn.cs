using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTomatoSpawn : MonoBehaviour
{
    SpriteRenderer sr;
    public Vector2 actualPos;
    private bool spawnedEnemy = false;
    GameObject CherryTomato;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        actualPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (sr.isVisible) //Si la camara esta viendo al objeto anclado a este script
        {
            if (spawnedEnemy == false)
            {     
                CherryTomato = Instantiate(Resources.Load("Prefabs/Enemies/CherryTomato") as GameObject);
                CherryTomato.transform.localPosition = new Vector2(actualPos.x, actualPos.y);
                spawnedEnemy = true; //Booleano para evitar que salgan sin parar.

                gameObject.transform.parent = CherryTomato.transform; //Se deja este objeto como hijo de tomate.
            }
        }
        else //Si la camara no esta viendo al objeto anclado a este script
        {
            gameObject.transform.parent = null; //Este objeto ya no es hijo de tomate
            transform.localPosition = actualPos; //Este objeto se devuelve a su posici�n determinada al inicio de la escena.
            Destroy(CherryTomato);
            spawnedEnemy = false;
        }
    }
}