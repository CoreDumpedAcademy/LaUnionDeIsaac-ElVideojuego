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

    private Rigidbody2D rb;
    private Animator anim;
    private KeyDrop keyDrop;

    public bool notInMap = true;
    public int value;
    private float count = 0.2f;


    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        keyDrop = GetComponent<KeyDrop>();

        isAttacking = false;
        notInMap = true;
        dashCooldown = skeletonCooldown;

        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void Update () {

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
                anim.SetBool("isAttacking", false);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("isAttacking", false);
                anim.SetFloat("YSpeed", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("isAttacking", false);
                anim.SetFloat("YSpeed", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("isAttacking", false);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (isAttacking == true)
        {
            if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("isAttacking", true);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("isAttacking", true);
                anim.SetFloat("YSpeed", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("isAttacking", true);
                anim.SetFloat("YSpeed", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", false);
                anim.SetBool("isAttacking", true);
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

    void TakeDamage()
    {
        speed = speed * Stats.slowdown;
        Player.playerHealth += Stats.robovida;
        health -= 10;
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
