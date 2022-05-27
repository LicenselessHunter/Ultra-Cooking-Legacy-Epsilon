using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMozzarella : MonoBehaviour
{

    SpriteRenderer Renderer;
    private Vector2 PosicionActual;
    private bool EnemigoSpawneado = false;
    GameObject Mozzarella;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        PosicionActual = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (Renderer.isVisible)
        {
            if (EnemigoSpawneado == false)
            {
                Mozzarella = Instantiate(Resources.Load("Prefabs/Enemies/Muzzarella") as GameObject);
                Mozzarella.transform.localPosition = new Vector2(PosicionActual.x, PosicionActual.y);
                EnemigoSpawneado = true;

                gameObject.transform.parent = Mozzarella.transform;
            }
        }
        else
        {
            gameObject.transform.parent = null;
            transform.localPosition = PosicionActual;
            Destroy(Mozzarella);
            EnemigoSpawneado = false;
        }
    }
}
