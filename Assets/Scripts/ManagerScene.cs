using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScene : MonoBehaviour
{
    //Sistema de Diccionario para paneles escalables, añadir otro panel al fondo del MainCanvas, se agregara solo al diccionario, y dependiendo de su indice de posicion agregar los botones.
    public Dictionary<GameObject, int> panels = new Dictionary<GameObject, int>();
    GameObject MainCanvas;
    public Image fade;

    public Text TextSuccessSignUp;
    public Text TextFailureSignUp;

    public Text TextFailureLogIn;

    void Awake()
    {
        MainCanvas = GameObject.Find("MainCanvas");
    }

    private void Start()
    {
        fade.CrossFadeAlpha(0f, 1f, false);
        
        int i = 1;
        foreach (Transform child in MainCanvas.transform)
        {
            panels.Add(child.gameObject, i);
            i++;
        }

        ChangePanel(1);
    }

    public void LoadScene(int scene)
    {
        fade.CrossFadeAlpha(1f, 1f, false);
        SceneManager.LoadScene(scene);
    }

    public void ChangePanel(int panelId)
    {
        if (panelId == 2) //Si el panel de registro/login es inicializado (Esto es para que los mensajes de usuario no queden en la pantalla una vez activados.
        {
            //Mensajes de error o exito de usuario
            TextFailureSignUp.gameObject.SetActive(false);
            TextSuccessSignUp.gameObject.SetActive(false);

            TextFailureLogIn.gameObject.SetActive(false);

        }
        foreach (var item in panels)
        {
            if(panelId == item.Value)
            {
                item.Key.SetActive(true);
            }
            else
            {
                item.Key.SetActive(false);
            }
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
