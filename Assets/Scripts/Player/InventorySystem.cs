using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory-related")]
    //GameObject List that gets populated when the player picks up the ingredients that the enemies drop.
    //public Dictionary<GameObject, int> inventory = new Dictionary<GameObject, int>();
    public List<(GameObject, string, int)> inventory = new List<(GameObject, string, int)>();
    public GameObject InventoryItemPrefab;
    public GameObject inventoryArea;
    public float lastItemPosY = 0;
    public float lastItemPosX = 0;

    void Start()
    {
        InventoryItemPrefab = Resources.Load("Prefabs/Player/InventoryItem") as GameObject;
        inventoryArea = GameObject.FindWithTag("InventoryCanvasArea");

        if (DBmanager.PlayerRestartLevel == true) //Si el jugador reinicio el nivel después de morir.
        {
            DBmanager.PlayerRestartLevel = false;
            StartCoroutine(QuantityRecovery());
        }
        else
        {
            StartCoroutine(SaveGame());
        }

    }

    void AddToInventory(GameObject collision)
    {
        bool alreadyExists = false;
        int itemIndex = 0;

        //List<GameObject, string, int> GameObject is the direct reference to the new item, string is the name of the clone drop, and int is the amount of the same object the player has in inventory.
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Item2 == collision.name)
            {
                alreadyExists = true;
                //Store the found item index to a variable
                itemIndex = i;
            }
        }

        //if the previous check finished and it didn't the object already, create it.
        if (!alreadyExists)
        {
            //Instantiate basic object prefab
            GameObject currentItem = Instantiate(InventoryItemPrefab);
            //Change item name from "InventoryItem" to the name of the drop
            currentItem.name = collision.gameObject.name;
            //Save the sprite from the drop to a variable
            Sprite collisionSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            //Get the image component from the instantiated item
            Image currentItemImage = currentItem.gameObject.GetComponent<Image>();
            //Change the clone's image to the drop's sprite
            currentItemImage.sprite = collisionSprite;
            //Add the item with the new image, name and quantity to the dictionary

            if (collision.name == "DropTomato(Clone)")
            {
                DBmanager.CherryTomato += 1;
                inventory.Add((currentItem, collision.name, DBmanager.CherryTomato));
            }

            if (collision.name == "DropMuzzarella(Clone)")
            {
                DBmanager.mozzarella += 1;
                inventory.Add((currentItem, collision.name, DBmanager.mozzarella));
            }

            if (collision.name == "DropOlive(Clone)")
            {
                DBmanager.OliveOil += 1;
                inventory.Add((currentItem, collision.name, DBmanager.OliveOil));
            }

            if (collision.name == "DropItalianSauce(Clone)")
            {
                DBmanager.Sauce += 1;
                inventory.Add((currentItem, collision.name, DBmanager.Sauce));
            }

        }
        else
        {
            //If the player hasn't collected 5 of these, add 1 to their inventory
            if (inventory[itemIndex].Item3 < 5)
            {
                if (collision.name == "DropTomato(Clone)")
                {
                    DBmanager.CherryTomato += 1;
                    var v = inventory[itemIndex];
                    v.Item3 = DBmanager.CherryTomato;
                    inventory[itemIndex] = v;
                }

                if (collision.name == "DropMuzzarella(Clone)")
                {
                    DBmanager.mozzarella += 1;
                    var v = inventory[itemIndex];
                    v.Item3 = DBmanager.mozzarella;
                    inventory[itemIndex] = v;
                }

                if (collision.name == "DropOlive(Clone)")
                {
                    DBmanager.OliveOil += 1;
                    var v = inventory[itemIndex];
                    v.Item3 = DBmanager.OliveOil;
                    inventory[itemIndex] = v;
                }

                if (collision.name == "DropItalianSauce(Clone)")
                {
                    DBmanager.Sauce += 1;
                    var v = inventory[itemIndex];
                    v.Item3 = DBmanager.Sauce;
                    inventory[itemIndex] = v;
                }

            }
            else
            {
                //GivePoints() or something to reward the player for killing an extra enemy even though their inventory is already full of such drop.
            }
        }

        //Visually update every inventory item in UI
        UpdateInventory();
    }

    void UpdateInventory()
    {
        //First quickly delete all existing items from the UI to refresh
        foreach (Transform child in inventoryArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        lastItemPosY = -46;

        //Add every GameObject in the dictionary to the UI
        foreach (var item in inventory)
        {
            //Instantiate the GameObject
            GameObject currentItem = Instantiate(item.Item1);
            //Set the Inventory object in canvas as parent
            currentItem.gameObject.transform.SetParent(inventoryArea.transform, false);
            //Get the RectTransform component from the clone
            RectTransform itemRectTransform = currentItem.GetComponent<RectTransform>();
            //Move into the new position using the las clone's Y position
            itemRectTransform.anchoredPosition = new Vector2(-74, lastItemPosY - 28);
            //Update the lastItemPosY for the next clone to use.
            lastItemPosY = itemRectTransform.offsetMin.y;
            //Get the child text object from the parent clone and change the value to the item count in inventory.
            Text itemCount = currentItem.transform.GetChild(0).GetComponent<Text>();
            itemCount.text = "x" + item.Item3;
            //If the player has 5 of the same item, mark it as complete.
            if (item.Item3 == 5)
            {
                currentItem.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyDrop")
        {
            Destroy(collision.gameObject);
            AddToInventory(collision.gameObject);
        }
    }

    IEnumerator SaveGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("CherryQuantity", DBmanager.CherryTomato);
        form.AddField("mozzarellaQuantity", DBmanager.mozzarella);
        form.AddField("OliveOilQuantity", DBmanager.OliveOil);
        form.AddField("SauceQuantity", DBmanager.Sauce);

        form.AddField("username", DBmanager.username);
        using (UnityWebRequest www = UnityWebRequest.Post("https://ultracookinge.000webhostapp.com/savegame.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("Game Saved");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

        if (DBmanager.CherryTomato != 0)
        {
            GameObject currentItem = Instantiate(InventoryItemPrefab);
            currentItem.name = "DropTomato(Clone)";
            GameObject tomatoPrefab = Resources.Load("Prefabs/Drops/DropTomato") as GameObject;

            Sprite collisionSprite = tomatoPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            Image currentItemImage = currentItem.gameObject.GetComponent<Image>();
            currentItemImage.sprite = collisionSprite;


            inventory.Add((currentItem, currentItem.name, DBmanager.CherryTomato));
        }

        if (DBmanager.mozzarella != 0)
        {
            GameObject currentItem = Instantiate(InventoryItemPrefab);
            currentItem.name = "DropMuzzarella(Clone)";
            GameObject tomatoPrefab = Resources.Load("Prefabs/Drops/DropMuzzarella") as GameObject;

            Sprite collisionSprite = tomatoPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            Image currentItemImage = currentItem.gameObject.GetComponent<Image>();
            currentItemImage.sprite = collisionSprite;


            inventory.Add((currentItem, currentItem.name, DBmanager.mozzarella));
        }

        if (DBmanager.OliveOil != 0)
        {
            GameObject currentItem = Instantiate(InventoryItemPrefab);
            currentItem.name = "DropOlive(Clone)";
            GameObject tomatoPrefab = Resources.Load("Prefabs/Drops/DropOlive") as GameObject;

            Sprite collisionSprite = tomatoPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            Image currentItemImage = currentItem.gameObject.GetComponent<Image>();
            currentItemImage.sprite = collisionSprite;


            inventory.Add((currentItem, currentItem.name, DBmanager.OliveOil));
        }

        if (DBmanager.Sauce != 0)
        {
            GameObject currentItem = Instantiate(InventoryItemPrefab);
            currentItem.name = "DropItalianSauce(Clone)";
            GameObject tomatoPrefab = Resources.Load("Prefabs/Drops/DropItalianSauce") as GameObject;

            Sprite collisionSprite = tomatoPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            Image currentItemImage = currentItem.gameObject.GetComponent<Image>();
            currentItemImage.sprite = collisionSprite;


            inventory.Add((currentItem, currentItem.name, DBmanager.Sauce));
        }

        UpdateInventory();
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
        }

        yield return StartCoroutine(SaveGame());

    }
}