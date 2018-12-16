using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVariables : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PlayerPrefs.SetInt("actualFloor", 2);
        PlayerPrefs.SetFloat("firstHealth", 200f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
