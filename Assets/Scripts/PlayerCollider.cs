using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {


    public static float hitCooldown=1f;
    

    // Use this for initialization
    void Start () {
        
        hitCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {

        hitCooldown = hitCooldown - Time.deltaTime;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Player.hasKey = true;
            
        }

        if (collision.gameObject.tag == "Portal")
        {
            
            Player.hasKey = false;
        }
    }
}
