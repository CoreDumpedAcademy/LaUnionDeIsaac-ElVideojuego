﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patamon : MonoBehaviour {

    public float speed; // la velocidad con la que perseguirá el miniboss al target
    private float health=30; // la vida a tiempo real del miniboss
    public float healthMax; // vida total del miniboss

    public float chaseRange;
    private Transform target; //el objetivo al que el miniboss perseguirá 
    public float stopDistance; //distancia mínima de separación entre miniboss y player
    public float retreatDistance; //distancia a partir de la cual el miniboss debe huir del jugador

    private float timeLeftBtwShots; //controla el numero de segundos que quedan para que dispare
    public float timeBtwShots; //tiempo entre disparo y disparo
    public GameObject projectile;

    private Animator anim;
    public bool notInMap = true;
    private float count = 0.2f;
    private float deathCont = 1f;
    public bool isTouchingWall;

    public GameObject objetos;

    public int value;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        // hacemos que el target sea el lugar donde se encuentra el jugador (GameObject con tag "Player")
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeLeftBtwShots = timeBtwShots;
        health = healthMax;
        deathCont = 1f;
        isTouchingWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (distance < chaseRange)
        {

            moveTwdPlayer(); // función que hace que persiga al jugador y huya de él 
            movement(); // función que controla las animaciones del boss (work in progress)



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

        //muerte si no se encuentra dentro del mapa
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
    }


    private void movement()
    {

        // si player a la derecha del boss y más importante eje x
        if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
        {
            anim.SetBool("Vertical", false);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }// si player encima del boss y más importante eje y
        else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
        {
            anim.SetBool("Vertical", true);
            anim.SetFloat("YSpeed", 1);
        }// si plater más abajo del boss y mas importante eje y
        else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
        {
            anim.SetBool("Vertical", true);
            anim.SetFloat("YSpeed", -1);
        }  // si player a la izquierda del boss y más importante eje x
        else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
        {
            anim.SetBool("Vertical", false);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


    }

    private void moveTwdPlayer() //movement towards player
    {
        if (Vector2.Distance(transform.position, target.position) > stopDistance) //mientras la distancia entre miniboss y jugador sea mayor que la stopDistance
        {  // movemos el miniboss a donde se encuentre el target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < stopDistance && (Vector2.Distance(transform.position, target.position) > retreatDistance))
        { //si el jugador se acerca a más de la distancia mínima pero no está lo suficientemente cerca para llegar a ser la distancia de "tirar pa atrás"
            transform.position = this.transform.position; //el miniboss se queda quieto
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        { //movemos al miniboss en dirección contraria al target
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }

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

    void TakeDamage()
    {
        speed = speed * Stats.slowdown;
        Player.playerHealth += Stats.robovida;
        health -= Stats.arrowDamage;
        if (health <= 0)
        {
            ObjetsDrop.pos = transform.position;
            objetos.GetComponent<ObjetsDrop>().Drop();

            // score
            Stats.score = Stats.score + value;

            Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
