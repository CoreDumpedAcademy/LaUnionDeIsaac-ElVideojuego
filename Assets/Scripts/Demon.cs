using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour {

    public float speed;
    public float health;
    public float demonDamage;

    private Transform target;
    public float chaseRange;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    private Rigidbody2D rb;
    private Animator anim;
    public bool notInMap = true;
    private float count = 0.2f;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        notInMap = true;

        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if(distance < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
         //Fin del movimiento

        //Ataque

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if(distanceToAttack < attackRange)
        {
            //Comprobar si ha pasado tiempo suficiente desde el ultimo ataque
            if(Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeDamage", demonDamage);
                //Guardar la ultima vez que ataco
                lastAttackTime = Time.time;
            }
            
        }
        //Fin del ataque

        count = count - Time.deltaTime;

        if(count <= 0 && notInMap == true)
        {
            Destroy(gameObject);
        }

        

        //Animaciones
        
        if(distance < chaseRange)
        {
            if(target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetFloat("XSpeed", 1);
                anim.SetBool("Vertical", false);
            }
            else if(target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetFloat("YSpeed", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetFloat("YSpeed", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetFloat("XSpeed", -1);
                anim.SetBool("Vertical", false);
            }

        }
        else
        {
            anim.SetFloat("XSpeed", 0);
            anim.SetFloat("YSpeed", 0);
        }
    }
       
    //Muerte del enemigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            TakeDamage();
        }

        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {

            notInMap = false;
        }
    }

    //Muerte del enemigo
    public void TakeDamage()
    {
        //Restar vida al enemigo
        health -= 10;
        if(health <= 0)
        {
            //Matar al enemigo
            Destroy(this.gameObject);
        }

    }
    //Fin de muerte del enemigo

    //Muestra el rango de vision del enemigo en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }




}
