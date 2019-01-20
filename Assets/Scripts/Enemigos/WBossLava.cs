using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBossLava : MonoBehaviour {

    public float damage;
    private Transform player;
    private Vector2 target;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }
	
	// Update is called once per frame
	void Update () {
		

   
	}
}
