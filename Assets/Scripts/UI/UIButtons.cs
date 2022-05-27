using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    bool paused;
    public GameObject PauseMenu;

    void Start()
    {
        //PauseMenu.SetActive(false);
        Pause(false);
    }

	public void RestartGame()
	{
        DBmanager.PlayerRestartLevel = true;
		Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
	}

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UnpauseGame()
    {
        Pause(true);
    }

    public void PauseGame()
    {
        Pause(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause(paused);
        }
    }

    void Pause(bool paused)
    {
        //If not paused, stop the game and show the pause menu.
        if(!paused)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            paused = true;
        }
        //If already paused, resume the game and hide the pause menu.
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            paused = false;
        }
    }
}