using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChefHouse : MonoBehaviour
{

    bool VictoryButton = false;
    public Text QuantityItaly;
    public Text PressVforStats;
    public GameObject StatisticsPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SaveVictory());
    }

    // Update is called once per frame

    void Update()
    {
        if (VictoryButton == true)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {

                Debug.Log("Mostrar victorias");
                StatisticsPanel.gameObject.SetActive(true);
                QuantityItaly.text = DBmanager.completeQuantity.ToString();
            }
                 
              
        }

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PressVforStats.gameObject.SetActive(true);
            VictoryButton = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PressVforStats.gameObject.SetActive(false);
            VictoryButton = false;
            StatisticsPanel.gameObject.SetActive(false);
        }
    }

    IEnumerator SaveVictory()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBmanager.username);
        form.AddField("VictoryQuantity", DBmanager.completeQuantity);

        using (UnityWebRequest www = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/saveVictory.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                Debug.Log(DBmanager.completeQuantity);

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

    }
}
