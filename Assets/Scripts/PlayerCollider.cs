using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private float touchDamage;
    private float fireballDamage;
    private float hitCooldown=1f;


    // Use this for initialization
    void Start () {
        
        hitCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {

        touchDamage = Demon.demonDamage;
        fireballDamage = WBossProjectile.fireballDamage;

        hitCooldown = hitCooldown - Time.deltaTime;
        Debug.Log(Demon.demonDamage);
        Debug.Log(WBossProjectile.fireballDamage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
            if (hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - touchDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);
                Debug.Log(Player.playerHealth);
                hitCooldown = 1f;
            }          
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - touchDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);
                Debug.Log(Player.playerHealth);
                hitCooldown = 1f;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Fireball")
        {
            if (hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - fireballDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);
                Debug.Log(Player.playerHealth);
                hitCooldown = 1f;
            }
        }

        if (collision.gameObject.tag == "Key")
        {
            Player.hasKey = true;
            
        }

        if (collision.gameObject.tag == "Portal")
        {
            Debug.Log("Perder llave");
            Player.hasKey = false;
        }
    }
}
