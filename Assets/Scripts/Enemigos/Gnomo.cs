﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnomo : MonoBehaviour
{
    public float speed;
    public float health;
    private float touchDamage=20;

    private Transform target;
    public float chaseRange;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    private Rigidbody2D rb;
    private Animator anim;
    public bool notInMap = true;
    private float count = 0.2f;
    private float deathCont = 1f;
    public bool isTouchingWall;

    private float timeLeftBtwShots; //controla el numero de segundos que quedan para que dispare
    public float timeBtwShots=4f; //tiempo entre disparo y disparo
    public GameObject projectile;

    public GameObject objetos;

    public int value;
    // Use this for initialization
    void Start()
    {

        timeLeftBtwShots = timeBtwShots;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        notInMap = true;
        deathCont = 1f;
        isTouchingWall = false;
        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (distance < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        //Fin del movimiento

        //Ataque

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if (distanceToAttack < attackRange)
        {
            if (timeLeftBtwShots <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
            {
                Instantiate(projectile, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
                timeLeftBtwShots = timeBtwShots;
            }
            else
            {
                timeLeftBtwShots -= Time.deltaTime;
            }

        }
        //Fin del ataque

        count = count - Time.deltaTime;

        if (count <= 0 && notInMap == true)
        {
            Destroy(gameObject);
        }

        deathCont -= Time.deltaTime;

        if (deathCont > 0 && isTouchingWall == true)
        {
            Destroy(gameObject);
        }

        //Animaciones

        if (distance < chaseRange)
        {
            if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetFloat("XSpeed", 1);
                anim.SetBool("Vertical", false);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerCollider.hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - touchDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);

                PlayerCollider.hitCooldown = 1f;
            }
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerCollider.hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - touchDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);

                PlayerCollider.hitCooldown = 1f;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
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
        speed = speed * Stats.slowdown;
        Player.playerHealth += Stats.robovida;
        //Restar vida al enemigo
        health -= Stats.damage;
        if (health <= 0)
        {
            ObjetsDrop.pos = transform.position;
            objetos.GetComponent<ObjetsDrop>().Drop();

            // score
            Stats.score = Stats.score + value;

            //Matar al enemigo
            Destroy(gameObject);
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



