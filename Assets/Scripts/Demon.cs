using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour {

    public float speed;
    public float health;
    public float damage;

    private Transform target;
    public float chaseRange;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    private Rigidbody2D rb;
    private Animator anim;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
         //Fin del movimiento

        //Ataque

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if(distanceToAttack < attackRange)
        {
            //Comprobar si ha pasado tiempo suficiente desde el ultimo ataque
            if(Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeDamage", damage);
                //Guardar la ultima vez que ataco
                lastAttackTime = Time.time;
            }
            
        }

        //Fin del ataque

        //Animaciones
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h > 0 && h > v)
        {
            anim.SetFloat("XSpeed", 1);
            anim.SetFloat("YSpeed", 0);
        }
        else if(h > 0 && h < v)
        {
            anim.SetFloat("YSpeed", 1);
            anim.SetFloat("XSpeed", 0);
        }
        else if(h < 0 && h < v)
        {
            anim.SetFloat("XSpeed", -1);
            anim.SetFloat("YSpeed", 0);
        }
        else if(h < 0 && h > v)
        {
            anim.SetFloat("YSpeed", -1);
            anim.SetFloat("XSpeed", 0);
        }
        else if(h == 0 && v == 0)
        {
            anim.SetFloat("XSpeed", 0);
            anim.SetFloat("YSpeed", 0);
        }
    }

    //Muerte del enemigo
    public void TakeDamage(int damage)
    {
        //Restar vida al enemigo
        health -= damage;
        if(health <= 0)
        {
            //Matar al enemigo
            Destroy(this.gameObject);
            
        }

    }
    //Fin de muerte del enemigo

  
}
