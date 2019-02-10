using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {


    public static float hitCooldown=1f;
    private float redHitCooldown;
    private bool setRed;
    private bool gnomohit = false;
    private float betweenColors;
    private float previousSpeed; //para el slowdown del slime
    private float timeLeftSlow; 
    public float timeSlow;
    public AudioSource AS;

    // Use this for initialization
    void Start () {
        timeSlow = 3f;
        timeLeftSlow = timeSlow;
        betweenColors = 0.25f;
        redHitCooldown = 1f;
        hitCooldown = 0;
        setRed = false;
        previousSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {

        if (gnomohit)
        {
            if (timeLeftSlow > 0)
            {
                timeLeftSlow -= Time.deltaTime;
            }
            else
            {
                timeLeftSlow = timeSlow;
                gnomohit = false;
            }
        }
        if (!gnomohit)
            Player.speed = previousSpeed;
      

        hitCooldown = hitCooldown - Time.deltaTime;
        

        if (setRed == true)
        {
            redHitCooldown = redHitCooldown - Time.deltaTime;


            if (redHitCooldown <= 0)
            {
                Player.rend.material.SetColor("_Color", Color.white);
                setRed = false;
                redHitCooldown = 1f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "GnomoParticles")
        {
            if (Player.speed != 2f)
            {
                gnomohit = true;
                previousSpeed = Player.speed;
                Player.speed = 2f;
            }
        }

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


        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (!AS.isPlaying)
            {
                AS.Play();
            }
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
