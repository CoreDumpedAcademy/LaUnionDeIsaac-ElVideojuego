using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public GameObject resumeButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Personal_1Start");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Personal_2Login");
    }

    public void Selector()
    {
        SceneManager.LoadScene("Personal_3Selector");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Personal_5Scene");
    }

    public void Score()
    {
        SceneManager.LoadScene("Personal_6Scoreboard");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        resumeButton.GetComponent<PauseMenu>().Resume();
    }
}
