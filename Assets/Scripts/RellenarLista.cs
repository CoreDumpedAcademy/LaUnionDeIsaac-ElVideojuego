using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RellenarLista : MonoBehaviour {

    public Text score;
    public Text user;
    public static string scoreDatos;
    public static string userDatos;
    public GameObject getRequest;
    

	// Use this for initialization
	void Start () {
        getRequest.GetComponent<GetRequest>().getRanking();
	}
	
	// Update is called once per frame
	void Update () {
        score.text = scoreDatos;
        user.text = userDatos;
	}

    public static void RellenarUser(string value)
    {
        userDatos = value;
    }

    public static void RellenarScore(string value)
    {
        scoreDatos = value;
    }
}
