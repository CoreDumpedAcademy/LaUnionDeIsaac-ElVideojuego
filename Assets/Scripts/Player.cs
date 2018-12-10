using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
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

    // Use this for initialization
    void Start () {

        //Traemos los componentes de player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dashTime = startDashTime;
        

    }
	
	// Update is called once per frame
	void Update () {


        //obtenemos las direcciones que va a obtener el jugador
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        

        
        //Hacemos que la velocidad del rigidbody sea la dirección * velocidad
        rb.velocity = new Vector2(h * speed,v*speed);

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
       




        //manejamos el dash del personaje
        if (isDashing == false)
        {

            dashCooldown = dashCooldown - Time.deltaTime;



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
                dashCooldown = startDashCooldown;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if(isDashing == true)
                {
                    if (h != 0 && v != 0)
                    {
                        rb.velocity = new Vector2(h * dashSpeed, v * dashSpeed/2);
                    }
                    else
                    {
                        rb.velocity = new Vector2(h * dashSpeed, v * dashSpeed);
                    }
                }
            }
        }       
	}
}
