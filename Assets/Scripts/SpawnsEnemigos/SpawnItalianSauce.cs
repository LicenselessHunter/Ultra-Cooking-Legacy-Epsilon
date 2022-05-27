using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItalianSauce : MonoBehaviour
{
    SpriteRenderer Renderer;
    private Vector2 PosicionActual;
    private bool EnemigoSpawneado = false;
    GameObject ItalianSauce;


    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        PosicionActual = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Renderer.isVisible)
        {

            if (EnemigoSpawneado == false)
            {
                ItalianSauce = Instantiate(Resources.Load("Prefabs/Enemies/ItalianSauce") as GameObject);
                ItalianSauce.transform.localPosition = new Vector2(PosicionActual.x, PosicionActual.y);
                EnemigoSpawneado = true;

                gameObject.transform.parent = ItalianSauce.transform;
            }

        }
        else
        {
         
            gameObject.transform.parent = null;
            transform.localPosition = PosicionActual;
            Destroy(ItalianSauce);
            EnemigoSpawneado = false;

        }
    }
}
