using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara : MonoBehaviour {

    public float value;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stats.cooldown = Stats.cooldown + value;
        Debug.Log("cooldown aumentado");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
