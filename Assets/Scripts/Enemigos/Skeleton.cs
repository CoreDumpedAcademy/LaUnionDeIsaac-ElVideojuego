using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {

    public float speed;
    public float health;

    private float skeletonCooldown;
    private float skeletonDamage;
    private float skeletonDashTime;
    public float startSkeletonDashTime;
    public float startSkeletonCooldown;

    private Transform target;
    public float skeletonChaseRange;
    public bool isDashing = false;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;
    private float deathCont = 1f;
    public bool isTouchingWall;

    private Rigidbody2D rb;
    private Animator anim;
    public bool notInMap = true;
    private float count = 0.2f;

    public GameObject objetos;

    public int value;
    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathCont = 1f;
        isTouchingWall = false;
        notInMap = true;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        skeletonDamage = Stats.skeletonDamage;

        //Movimiento del esqueleto

        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (distance < skeletonChaseRange)
        {
            if (isDashing == false)
            {

                skeletonCooldown = skeletonCooldown - Time.deltaTime;



                if (skeletonCooldown <= 0)
                {
                    isDashing = true;
                    skeletonDashTime = startSkeletonDashTime;
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
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                        //Animaciones
                        if(target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
                        {
                            anim.SetFloat("X", 1);
                            anim.SetBool("LH", true);
                        }
                        else if(target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
                        {
                            anim.SetFloat("X", -1);
                            anim.SetBool("LH", true);
                        }
                        else if(target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
                        {
                            anim.SetFloat("Y", 1);
                            anim.SetBool("LH", false);
                        }
                        else if(target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
                        {
                            anim.SetFloat("Y", -1);
                            anim.SetBool("LH", false);
                        }
                    }
                }
            }
          
        }


        //Fin del movimiento del esqueleto

        //Ataque

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if (distanceToAttack < attackRange)
        {
            //Comprobar si ha pasado tiempo suficiente desde el ultimo ataque
            if (Time.time > lastAttackTime + attackDelay)
            {
                //Guardar la ultima vez que ataco
                lastAttackTime = Time.time;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerCollider.hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - skeletonDamage;
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
                Player.playerHealth = Player.playerHealth - skeletonDamage;
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
    public void TakeDamage()
    {
        speed = speed * Stats.slowdown;
        Player.playerHealth += Stats.robovida;
        //Restar vida al enemigo
        health -= Stats.arrowDamage;
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
        Gizmos.DrawWireSphere(transform.position, skeletonChaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
