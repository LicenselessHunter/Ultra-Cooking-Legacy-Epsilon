using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Registration : MonoBehaviour
{

    public Button BttnRegister;
    public InputField NameField;
    public InputField PasswordField;


    public Text TextSuccess;
    public Text TextFailure;

    public void RegisterButton()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", NameField.text);
        form.AddField("password", PasswordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text[0] == '0') //Si el primer caracter de text es 0. Que significara que nos logeamos exitosamente.
            {
                Debug.Log("Registrado exitosamente");
                
                //Mensajes de error o exito de usuario
                TextFailure.gameObject.SetActive(false);
                TextSuccess.gameObject.SetActive(true);
                
            }
            else
            {
                Debug.Log("User Error: " + www.downloadHandler.text); //Si algo sale mal. Va a mostrar el echo de error del PHP.
                if (www.downloadHandler.text == "3: Name already exists")
                {

                    //Mensajes de error o exito de usuario
                    TextSuccess.gameObject.SetActive(false);
                    TextFailure.gameObject.SetActive(true);
                }

            }
        }
    }

    public void VerifyInputs() //Este método va a servir para aceptar los forms bajo ciertas condiciones. Condiciones como: La cantidad de caracteres en un nombre, tener ciertos caracteres en la contraseña etc.
    {
        BttnRegister.interactable = (NameField.text.Length >= 5 && PasswordField.text.Length >= 5); //Si estas condiciones se cumplen, se va a poder interactuar con el botón de submit.
    }



}
