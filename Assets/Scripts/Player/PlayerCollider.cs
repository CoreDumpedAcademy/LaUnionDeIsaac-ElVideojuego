using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {


    public static float hitCooldown=1f;
    private float redHitCooldown;
    private bool setRed;
    private float betweenColors;
    private float previousSpeed; //para el slowdown del slime

    // Use this for initialization
    void Start () {
        betweenColors = 0.25f;
        redHitCooldown = 1f;
        hitCooldown = 0;
        setRed = false;
	}
	
	// Update is called once per frame
	void Update () {

       Debug.Log("Health:" + Player.playerHealth);
      

        hitCooldown = hitCooldown - Time.deltaTime;
        

        if (setRed == true)
        {
            redHitCooldown = redHitCooldown - Time.deltaTime;


            if (redHitCooldown <= 0)
            {
                Debug.Log("Blanco");
                Player.rend.material.SetColor("_Color", Color.white);
                setRed = false;
                redHitCooldown = 1f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "AngelGlue")
        {
            if (Player.speed != 2f)
            {
                previousSpeed = Player.speed;
                Player.speed = 2f;
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

        if (collision.gameObject.tag == "HealthPotion")
        {
            if (Player.playerHealth + Stats.health * Stats.potionRegeneration <= Stats.health)
            {
                Player.playerHealth = Player.playerHealth + Stats.health * Stats.potionRegeneration;
            }
            else
            {
                Player.playerHealth = Stats.health;
            }
        }


        //Las  colisiones con el HellSlimeGlue están incluídas en "enemy"
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "EpicAttack" || collision.gameObject.tag == "LavaGlue")
        {
            if (setRed == false)
            {
                Player.rend.material.SetColor("_Color", Color.red);
                setRed = true;
            }
            Debug.Log(setRed);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AngelGlue")
        {
            Player.speed = previousSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Las  colisiones con el HellSlimeGlue están incluídas en "enemy"
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "EpicAttack" || collision.gameObject.tag == "LavaGlue")
        {
            if (setRed == false)
            {
                Player.rend.material.SetColor("_Color", Color.red);
                setRed = true;
            }

            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Las  colisiones con el HellSlimeGlue están incluídas en "enemy"
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "EpicAttack" || collision.gameObject.tag == "LavaGlue")
        {
            if (setRed == false)
            {
                Player.rend.material.SetColor("_Color", Color.red);
                setRed = true;
            }

            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Las  colisiones con el HellSlimeGlue están incluídas en "enemy"
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "EpicAttack" || collision.gameObject.tag == "LavaGlue")
        {
            if (setRed == false)
            {
                Player.rend.material.SetColor("_Color", Color.red);
                setRed = true;
            }

           
           
        }
    }
}
