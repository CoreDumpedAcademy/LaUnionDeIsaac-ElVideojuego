using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedySkeleton : MonoBehaviour {

    public float speed;
    public float health;
    public float skeletonDamage;

    private float dashCooldown;
    private float skeletonCooldown;
    private float skeletonDashTime;
    public float startSkeletonDashTime;
    public float startSkeletonCooldown;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    public bool isAttacking;

    private Transform target;
    private Vector2 playerPosition;
    public bool isDashing = false;
    public GameObject projectile;

    public GameObject EA2;
    private float EPICtimeLeftBtwShots; //controla el numero de segundos que quedan para que dispare
    public float EPICtimeBtwShots;

    private Rigidbody2D rb;
    private Animator anim;
    private KeyDrop keyDrop;

    public bool notInMap = true;
    public int value;
    private float count = 0.2f;

    private float cont;
    private float deathCont;
    private bool isTouchingWall;


    // Use this for initialization
    void Start () {

        cont = 0.2f;
        deathCont = 1f;
        isTouchingWall = false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        keyDrop = GetComponent<KeyDrop>();

        EPICtimeLeftBtwShots = EPICtimeBtwShots;

        isAttacking = false;
        notInMap = true;
        dashCooldown = skeletonCooldown;

        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void Update () {



        if (EPICtimeLeftBtwShots <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
        {
            Instantiate(EA2, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            EPICtimeLeftBtwShots = EPICtimeBtwShots;
            Debug.Log("EA2");
        }
        else
        {
            EPICtimeLeftBtwShots -= Time.deltaTime;
        }

        count = count - Time.deltaTime;

        if (count <= 0 && notInMap == true)
        {
            EpicGenerator.maxEnemies = false;
            EpicGenerator.enemySpawned = false;
            Destroy(gameObject);
        }

        deathCont -= Time.deltaTime;

        if (deathCont > 0 && isTouchingWall == true)
        {
            EpicGenerator.maxEnemies = false;
            EpicGenerator.enemySpawned = false;
            Destroy(gameObject);
        }

        if (isDashing == false)
        {

            skeletonCooldown = skeletonCooldown - Time.deltaTime;
            isAttacking = false;

            if (skeletonCooldown <= 0)
            {
                isDashing = true;
                skeletonDashTime = startSkeletonDashTime;
                playerPosition = target.position;
            }

        }
        else
        {
            if (skeletonDashTime <= 0)
            {
                isDashing = false;
                skeletonCooldown = startSkeletonCooldown;
                
            }
            else
            {
                skeletonDashTime -= Time.deltaTime;

                if (isDashing == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

                    //Fin del movimiento

                    //Ataque
                    isAttacking = true;
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
                
            }
        }

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if (distanceToAttack < attackRange)
        {
            //Comprobar si ha pasado tiempo suficiente desde el ultimo ataque
            if (Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeDamage", skeletonDamage);
                //Guardar la ultima vez que ataco
                lastAttackTime = Time.time;
            }

        }
        //Fin del ataque

        if (isAttacking == false)
        {
            if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("IsAttacking", false);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("IsAttacking", false);
                anim.SetFloat("YSpeed", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("IsAttacking", false);
                anim.SetFloat("YSpeed", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("IsAttacking", false);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (isAttacking == true)
        {
            if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("IsAttacking", true);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("IsAttacking", true);
                anim.SetFloat("YSpeed", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("IsAttacking", true);
                anim.SetFloat("YSpeed", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("IsAttacking", true);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

      

        count = count - Time.deltaTime;

        if (count <= 0 && notInMap == true)
        {
            EpicGenerator.maxEnemies = false;
            Destroy(gameObject);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
        }
    }

    void TakeDamage()
    {
        speed = speed * Stats.slowdown;
        Player.playerHealth += Stats.robovida;
        health -= Stats.damage;
        if (health <= 0)
        {
            keyDrop.SpawnKey();

            // score
            Stats.score = Stats.score + value;

            Destroy(gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
