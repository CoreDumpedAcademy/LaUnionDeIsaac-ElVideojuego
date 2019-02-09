using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool gameIsPaused;
    public bool scoreIsInDisplay;
    public GameObject canvasPause;
    public GameObject canvasScoreGame;
    public GameObject rellenarStats;
    public GameObject canvasDead;
    private AudioSource AS;

    // Use this for initialization
    void Start ()
    {
        AS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rellenarStats.GetComponent<UserValues>().Rellenar();
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            rellenarStats.GetComponent<UserValues>().RellenarScore();
        }
        
        if (Player.playerIsDead)
        {
            rellenarStats.GetComponent<UserValues>().RellenarScore();
            canvasDead.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        AS.Play();
        canvasPause.SetActive(false);
        canvasScoreGame.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        scoreIsInDisplay = false;
    }

    public void Pause()
    {
        AS.Play();
        canvasPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Score()
    {
        AS.Play();
        if (scoreIsInDisplay)
        {
            scoreIsInDisplay = false;
            canvasPause.SetActive(true);
            canvasScoreGame.SetActive(false);
        }
        else
        {
            scoreIsInDisplay = true;
            canvasPause.SetActive(false);
            canvasScoreGame.SetActive(true);
        }
    }
}
