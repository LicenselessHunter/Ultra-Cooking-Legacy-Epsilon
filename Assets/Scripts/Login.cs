using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public Button BttnLogin;
    public InputField NameField;
    public InputField PasswordField;

    public Text TextFailure;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", NameField.text);
        form.AddField("password", PasswordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text[0] == '0') //Si el primer caracter de text es 0. Que significara que nos logeamos exitosamente.
            {
                DBmanager.username = NameField.text;
                Debug.Log("Logeado exitosamente");
                yield return StartCoroutine(QuantityRecovery());
                
            }
            else
            {
                Debug.Log("User Error: " + www.downloadHandler.text); //Si algo sale mal. Va a mostrar el echo de error del PHP.
                //Mensajes de error o exito de usuario
                TextFailure.gameObject.SetActive(true);

            }

        }
    }

    IEnumerator QuantityRecovery()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.username);

        using (UnityWebRequest request = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/ItemRecovery.php", form))
        {
            yield return request.SendWebRequest();

            if (request.downloadHandler.text[0] == '0') //Si el primer caracter de text es 0. Que significara que nos logeamos exitosamente.
            {
                DBmanager.CherryTomato = int.Parse(request.downloadHandler.text.Split('\t')[1]);
                DBmanager.OliveOil = int.Parse(request.downloadHandler.text.Split('\t')[2]);
                DBmanager.mozzarella = int.Parse(request.downloadHandler.text.Split('\t')[3]);
                DBmanager.Sauce = int.Parse(request.downloadHandler.text.Split('\t')[4]);
                Debug.Log("Funciona");

            }
            else
            {
                Debug.Log("User Error: " + request.downloadHandler.text); //Si algo sale mal. Va a mostrar el echo de error del PHP.

            }

            yield return StartCoroutine(VictoryRecovery());

        }
    }

    IEnumerator VictoryRecovery()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.username);

        using (UnityWebRequest request = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/levelRecovery.php", form))
        {
            yield return request.SendWebRequest();

            if (request.downloadHandler.text[0] == '0') //Si el primer caracter de text es 0. Que significara que nos logeamos exitosamente.
            {
                DBmanager.completeQuantity = int.Parse(request.downloadHandler.text.Split('\t')[1]);

            }
            else
            {
                Debug.Log("User Error: " + request.downloadHandler.text); //Si algo sale mal. Va a mostrar el echo de error del PHP.

            }
            SceneManager.LoadScene(2); //Ir a la escena de la casa del chef
        }
    }

    public void VerifyInputs() //Este método va a servir para aceptar los forms bajo ciertas condiciones. Condiciones como: La cantidad de caracteres en un nombre, tener ciertos caracteres en la contraseña etc.
    {
        BttnLogin.interactable = (NameField.text.Length >= 5 && PasswordField.text.Length >= 5); //Si estas condiciones se cumplen, se va a poder interactuar con el botón de submit.
    }

}
