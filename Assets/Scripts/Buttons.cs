using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject resumeButton;
    public GameObject player;
    private AudioSource AS;
    public static bool stopMMMusic;
    // Use this for initialization
    void Start()
    {
        stopMMMusic = false;
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitToMenu()
    {
        AS.Play();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        AS.Play();
        Stats.score = Stats.scoreD;
        Stats.cadencia = Stats.cadenciaD;
        Stats.robovida = Stats.robovidaD;
        Stats.slowdown = Stats.slowdownD;
        Stats.cooldown = Stats.cooldownD;
        Stats.health = Stats.healthD;
        Stats.speed = Stats.speedD;
        Stats.damage = Stats.damageD;
        Stats.dropRate = Stats.dropRateD;
        Stats.potionDropRate = Stats.potionDropRateD;
        Player.playerIsDead = false;
        
        SceneManager.LoadScene("Login");
        Time.timeScale = 1f;
    }

    public void Selector()
    {
        AS.Play();
        SceneManager.LoadScene("CharacterSelector");
        Time.timeScale = 1f;
    }

    public void LoadGameScene()
    {
        stopMMMusic = true;
        AS.Play();
        SceneManager.LoadScene("Floor1");
        Time.timeScale = 1f;
    }

    public void Score()
    {
        AS.Play();
        SceneManager.LoadScene("Scoreboard");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        AS.Play();
        Application.Quit();
    }

    public void Resume()
    {
        AS.Play();
        resumeButton.GetComponent<PauseMenu>().Score();
    }

    public void Respawn()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Floor");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = player.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Debug.Log(distance);
        //player.transform.position = closest.transform.position;
    }
}
