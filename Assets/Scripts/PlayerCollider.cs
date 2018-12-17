using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private float touchDamage;
    private float fireballDamage;
    private float epicAttackDamage;
    private float hitCooldown=1f;
    

    // Use this for initialization
    void Start () {
        
        hitCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {

        touchDamage = Demon.demonDamage;
        fireballDamage = WBossProjectile.fireballDamage;
        epicAttackDamage = EpicAttack.epicAttackDamage;
        hitCooldown = hitCooldown - Time.deltaTime;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
            if (hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - touchDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);
                
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
                
                hitCooldown = 1f;
            }
        }

        if (collision.gameObject.tag == "EpicAttack")
        {
            if (hitCooldown <= 0 && EpicAttack.touchingEpicAttack == true)
            {
                Player.playerHealth = Player.playerHealth - epicAttackDamage;
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
            
            Player.hasKey = false;
        }
    }
}
