using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOlivo : MonoBehaviour
{

    SpriteRenderer Renderer;
    public Vector2 PosicionActual;
    private bool EnemigoSpawneado = false;
    GameObject Olivo;



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

                Olivo = Instantiate(Resources.Load("Prefabs/Enemies/Olive") as GameObject);
                Olivo.transform.localPosition = new Vector2(PosicionActual.x, PosicionActual.y);
                EnemigoSpawneado = true;

                gameObject.transform.parent = Olivo.transform;
            }

        }
        else
        {
  
            gameObject.transform.parent = null;

            transform.localPosition = PosicionActual;
            Destroy(Olivo);
            EnemigoSpawneado = false;
        }

        
    }
}
