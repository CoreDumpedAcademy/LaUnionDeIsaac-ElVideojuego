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
        
        //aplicamos las direcciones para que el animador sepa cuando hacer qué animación
        anim.SetFloat("SpeedX", h);
        anim.SetFloat("SpeedY", v);

        if (isDashing == false)
        {

            dashCooldown = dashCooldown - Time.deltaTime;

            Debug.Log(dashCooldown);

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
