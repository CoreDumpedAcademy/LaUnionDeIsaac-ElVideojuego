using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerLoad = 0.1f;
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 mov;
    private float dashTime;
    public float startDashCooldown;
    private float dashCooldown;
    public float dashSpeed;
    public float startDashTime;
    public bool isDashing = false;
    public static float playerHealth;
    public static int score;
    public GameObject PSpawner;
    public static float speed;

    public static Renderer rend;

    // Variables duplicadas para poder modificarlas en unity y a la vez poder acceder al valor sin un getComponent. by raular4322 (que práctico, suena bastante útil, bien hecho raúl). 
    public bool playerHasKey; //raular4322
    public bool isPlayerDead;
    public static bool hasKey;
    public Slider healthBar;
    public static bool playerIsDead; // raular4322
    public bool notInMap = true;
    
    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.white);
        speed = Stats.speed;
        playerLoad = 0.1f;
        hasKey = false;
        isPlayerDead = false;
        dashTime = Stats.cooldown;
        Time.timeScale = 1f;

        // Traemos los componentes de player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        // Le proporcionamos la vida inicial al personaje
        PlayerPrefs.SetFloat("maxHealth", Stats.health);
        playerHealth = PlayerPrefs.GetFloat("firstHealth");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // display de la vida del personaje
        healthBar.value = playerHealth / Stats.health;

        // checks for a valid player spawn position
        playerLoad -= Time.deltaTime;
        if (playerLoad <= 0 && notInMap == true)
        {
            Instantiate(PSpawner);
            transform.position = PlayerSpawner.playerSpawn;
        }
        
        //obtenemos las direcciones que va a obtener el jugador
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        //Hacemos que la velocidad del rigidbody sea la dirección * velocidad
        rb.velocity = new Vector2(h * speed, v* speed);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
            anim.SetBool("LH", false);
            anim.SetFloat("SpeedY", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
            anim.SetBool("LH", false);
            anim.SetFloat("SpeedY", -1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
            anim.SetBool("LH", true);
            anim.SetFloat("SpeedX", 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
            anim.SetBool("LH", true);
            anim.SetFloat("SpeedX", -1);
        }
        else if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)&& !Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey(KeyCode.LeftArrow))
        {
            //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetBool("LH", true);
            }
            else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                anim.SetBool("LH", false);
            }
            anim.SetFloat("SpeedX", h);
            anim.SetFloat("SpeedY", v);
        }
        
        //Muerte del jugador
        if(playerHealth <= 0)
        {
            playerIsDead = true;
            gameObject.SetActive(false);
        }
        else
        {
            playerIsDead = false;
            gameObject.SetActive(true);
        }    

        //manejamos el dash del personaje
        if (isDashing == false)
        {
            dashCooldown -= Time.deltaTime;
            if (dashCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isDashing = true;
                    dashTime = startDashTime;
                }
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                isDashing = false;
                dashCooldown = Stats.cooldown;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if(isDashing == true)
                {
                    if (h != 0 && v != 0)
                    {
                        rb.velocity = new Vector2(h * dashSpeed, v * dashSpeed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(h * dashSpeed, v * dashSpeed);
                    }
                }
            }
        }       
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            notInMap = false;
        }
    }
}
