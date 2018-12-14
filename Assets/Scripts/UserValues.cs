using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserValues : MonoBehaviour {

    public Text nickText;
    public Text stats;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    
    public void SaveUsername()
    {
        Stats.username = nickText.text;
        Debug.Log(Stats.username);
    }

    public void Avatar1()
    {
        Stats.avatar = 1;
        Debug.Log("avatar 1");
    }

    public void Avatar2()
    {
        Stats.avatar = 2;
        Debug.Log("avatar ´2");
    }

    public void Avatar3()
    {
        Stats.avatar = 3;
        Debug.Log("avatar 3");
    }

    public void Avatar4()
    {
        Stats.avatar = 4;
        Debug.Log("avatar 4");
    }

    public void Rellenar()
    {
        stats.text = "\n" + Stats.cadencia + "\n" + Stats.robovida + "\n" + Stats.slowdown + "\n" + Stats.cooldown + "\n" + Stats.health + "\n" + Stats.speed;
        Debug.Log("\n" + Stats.cadencia + "\n" + Stats.robovida + "\n" + Stats.slowdown + "\n" + Stats.cooldown + "\n" + Stats.health + "\n" + Stats.speed);
    }

    public static string GetName()
    {
        return Stats.username;
    }

    public static int GetScore()
    {
        return Stats.score;
    }
}
