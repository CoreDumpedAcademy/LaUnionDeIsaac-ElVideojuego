using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour {

    public GameObject key;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("tiene una llave" + Player.hasKey);
        if (Player.hasKey)
        {
            key.SetActive(true);
        }
        else
        {
            key.SetActive(false);
        }
	}
}
