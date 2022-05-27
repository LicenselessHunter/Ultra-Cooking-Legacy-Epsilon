using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	//Dictionary that gets populated with the cloned Hearts in the UI.
	public Dictionary<GameObject, int> hearts = new Dictionary<GameObject, int>();
	
	//Dictionary that gets populated with the projectiles and damage that can be received. Those values will be loaded from a JSON in the future.
	public Dictionary<string, int> projectiles = new Dictionary<string, int>();

	//Dictionary that gets populated with the enemies and damage that can be received. The difference with projectiles is that the content of the matrix will not be deleted upon collision, this is useful when enemy colliders were being destroyed like projectiles.
	public Dictionary<string, int> enemies = new Dictionary<string, int>();
	
	[Header("Health")]
	public int maxHealth;						//Max health the playher has
	public int health;							//Current health of the player
	
	public bool canBeDamaged;					//Bool that determines if the player can be hit
	private float damageShield;					//Duration of the hit immunity
	public GameObject immuneShield;				//Immune shield object to display immunity after getting hit. Prone to be replaced
	string lastHit; 							//This is overwritten every time the player gets hit, so the DeathUI can show the reason why the player died.

	[Header("UI-Related")]
	public GameObject DeathUI;					//DeathUI object, is set active once the player dies
	private int heartCounter = 0;				//Amount of hearts, depending on the total maxHealth. Determined on GetMaxHealthState()
	public GameObject heartPrefab;				//Heart prefab to be instantiated
	public GameObject heartArea;				//Parent where the heart prefabs get instantiated
	public GameObject heartBG;					//Background image for hearts that gets scaled depending on the amount of hearts
	public float lastHeartLeftPos = 0;			//Left position of the last heart instantiated in GetMaxHealthState()
	public Text deathReason;					//Text that displays the reason the player died

	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer sr;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		
		heartPrefab = Resources.Load("Prefabs/Player/Heart") as GameObject;
		heartArea = GameObject.FindWithTag("HPCanvasArea");

		health = maxHealth;
		GetMaxHealthState();

		PopulateDamageMatrix();
	}

    void Start()
    {
		immuneShield.SetActive(false);
		DeathUI.SetActive(false);
		GetHealthState();
		damageShield = 0;
    }

	void Update()
	{
		//Dado que este juego debe correr sobre HTML5 en un servidor web muy limitado, casi ningun calculo u operacion transcurre durante cada frame, todo lo que consta al player esta programado de esa forma con ese objetivo.
		damageShield -= Time.deltaTime;
		if(damageShield <= 0)
		{
			canBeDamaged = true;
		}
	}

	void PopulateDamageMatrix()
	{
		//This section populates the "enemies" matrix, where the content is not deleted upon collision, data should be loaded from a JSON in the future.
		enemies.Add("OliveLowerHalf", 10);
		enemies.Add("CherryTomatoHitbox", 5);

		//This function populates the "projectiles" matrix, data should be loaded from a JSON in the future.
		projectiles.Add("OliveShot(Clone)", 5);
		projectiles.Add("MuzzarellaShot(Clone)", 5);
		projectiles.Add("ItalianSauceShot_0(Clone)", 5);
		projectiles.Add("ItalianSauceShot_1(Clone)", 5);
		projectiles.Add("ItalianSauceShot_2(Clone)", 5);
		projectiles.Add("ItalianSauceShot_3(Clone)", 5);
	}

	public void hurtPlayer(int amount)
	{
		if(canBeDamaged)
		{
			if ((health - amount) <= 0)
			{
				health = 0;
				GetHealthState();
				KillPlayer();
			}
			else
			{
				health -= amount;
				StartCoroutine(ShowDamage());
				GetHealthState();
				canBeDamaged = false;
				damageShield = 2;
				StartCoroutine(ShowImmunity());
			}
		}
	}

	IEnumerator ShowImmunity()
	{
		immuneShield.SetActive(true);
		yield return new WaitForSeconds(damageShield);
		immuneShield.SetActive(false);
	}

	IEnumerator ShowDamage()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		sr.color = Color.white;
	}

	void KillPlayer()
	{
		GetComponent<PlayerMovement>().enabled = false;
		GetComponent<PlayerMovement>().canMove = false;
		anim.SetBool("Dead", true);
		deathReason.text = "You died because of "+lastHit;
		DeathUI.SetActive(true);
	}

	void GetHealthState()
	{
		//So health can be as much as maxHealth
		if(health > maxHealth)
		{
			health = maxHealth;
		}

		int i = 0;

		//Update heart count
		if(health > 0)
		{
			foreach (var item in hearts)
			{
				GameObject heart = item.Key;
				GameObject rightHalf = heart.transform.GetChild(2).gameObject;
				GameObject leftHalf = heart.transform.GetChild(1).gameObject;
				if(((item.Value*10)-5) > health)
				{
					leftHalf.SetActive(false);
				}
				if(item.Value*10 > health)
				{
					rightHalf.SetActive(false);
				}
				i++;
			}
		}
	}

	void GetMaxHealthState()
	{
		//Destroy all the CHILDREN. In case a maxHealth power-up is developed, hearts are refreshed every time maxHealth changes.
		foreach (Transform child in heartArea.transform) 
		{
			GameObject.Destroy(child.gameObject);
		}

		//Get the RectTransform of the hearts background in the canvas. This is with the objective of scaling the background image depending on the amount of hearts. Pretty cool.
		RectTransform heartBGRT = heartBG.GetComponent<RectTransform>();
		//Determine the initial size of the BG Image with just 1 heart.
		int toBeMovedRight = 1777;

		for (int i = 0; i < maxHealth; i++)
		{
			if(i%10==0)
			{
				//Instantiate the prefab
				GameObject currentHeart = Instantiate(heartPrefab);
				//Set HPArea as the clone's parent
				currentHeart.transform.SetParent(heartArea.transform, false);
				//Get the clone's RectTransform
				RectTransform heartRectTransform = currentHeart.GetComponent<RectTransform>();
				//Move the clone relative to the las clone's position
				heartRectTransform.anchoredPosition = new Vector2(lastHeartLeftPos + 110, -70);
				//Add 1 to the heartCounter for the dictionary
				heartCounter += 1;
				//Add the current clone to the dictionary
				hearts.Add(currentHeart, heartCounter);
				//Set the Left Position of the current clone as the offset for the next one.
				lastHeartLeftPos = heartRectTransform.offsetMin.x;
			}
		}

		//Calculate the final Right int for the Image with all the hearts counted.
		toBeMovedRight -= (heartCounter)*74-40;
		//Enlarge the Right property of the heart background depending on the amount of hearts.
		heartBGRT.offsetMax = new Vector2(-toBeMovedRight, heartBGRT.offsetMax.y);
		
		GetHealthState();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(projectiles.ContainsKey(collision.name))
		{
			int hit = projectiles[collision.name];
			hurtPlayer(hit);
			Destroy(collision.gameObject);
			Debug.Log("Hit="+hit+", Cause="+collision.name);
			lastHit = collision.name;
		}

		if(enemies.ContainsKey(collision.name))
		{
			int hit = enemies[collision.name];
			hurtPlayer(hit);
			Debug.Log("Hit="+hit+", Cause="+collision.name);
			lastHit = collision.name;
		}
	}
}