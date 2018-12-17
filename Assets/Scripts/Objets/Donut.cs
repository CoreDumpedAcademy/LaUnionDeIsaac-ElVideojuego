using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour {

    public float value;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("health aumentado");
        if (collision.gameObject.tag == "Player")
        {
            Stats.health = Stats.health + value;
            Destroy(gameObject);
        }
    }
}
