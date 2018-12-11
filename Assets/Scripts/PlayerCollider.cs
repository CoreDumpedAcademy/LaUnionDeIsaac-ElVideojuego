using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private float touchDamage;
    private float fireballDamage;
    public PlayerHealth PH;

    // Use this for initialization
    void Start () {
        //touchDamage = Demon.demonDamage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Player.playerHealth = Player.playerHealth - touchDamage;
            PH.SetHealth(Player.playerHealth);
        }

        if (collision.gameObject.tag == "Fireball")
        {
            Player.playerHealth = Player.playerHealth - fireballDamage;
            PH.SetHealth(Player.playerHealth);
        }
    }
}
