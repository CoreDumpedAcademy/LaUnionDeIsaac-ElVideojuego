using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saco : MonoBehaviour {

    public float value;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("robovida aumentado");
        if (collision.gameObject.tag == "Player")
        {
            Stats.robovida = Stats.robovida + value;
            Destroy(gameObject);
        }
    }
}
