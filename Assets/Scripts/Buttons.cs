using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject resumeButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Stats.score = Stats.scoreD;
        Stats.cadencia = Stats.cadenciaD;
        Stats.robovida = Stats.robovidaD;
        Stats.slowdown = Stats.slowdownD;
        Stats.cooldown = Stats.cooldownD;
        Stats.health = Stats.healthD;
        Stats.speed = Stats.speedD;
        Stats.damage = Stats.damageD;
        Player.playerIsDead = false;

        SceneManager.LoadScene("Login");
        Time.timeScale = 1f;
    }

    public void Selector()
    {
        SceneManager.LoadScene("CharacterSelector");
        Time.timeScale = 1f;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Floor1");
        Time.timeScale = 1f;
    }

    public void Score()
    {
        SceneManager.LoadScene("Scoreboard");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        resumeButton.GetComponent<PauseMenu>().Score();
    }
}
