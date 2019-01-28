using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed;
    public float health;
    private float touchDamage;

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

    public float slimeGlueDelay;
    public GameObject objetos;
    public GameObject slimeGlue;
    public int value;
    // Use this for initialization
    void Start()
    {

        slimeGlueDelay = 1f;
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

        
        slimeGlueDelay = slimeGlueDelay - Time.deltaTime;

        if (slimeGlueDelay <= 0)
        {
            Instantiate(slimeGlue, transform.position, Quaternion.identity);
            slimeGlueDelay = 1f;
        }

        touchDamage = Stats.slimeDamage;

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



        //Animaciones

        if (distance < chaseRange)
        {
            if (target.position.x > transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetFloat("SpeedX", 1);
                anim.SetBool("Vertical", false);
            }
            else if (target.position.y > transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetFloat("SpeedY", 1);
            }
            else if (target.position.y < transform.position.y && Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetBool("Vertical", true);
                anim.SetFloat("SpeedY", -1);
            }
            else if (target.position.x < transform.position.x && Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                anim.SetFloat("SpeedX", -1);
                anim.SetBool("Vertical", false);
            }

        }
        else
        {
            anim.SetFloat("SpeedX", 0);
            anim.SetFloat("SpeedY", 0);
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
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
