using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnomo : MonoBehaviour
{
    public static float newspeed= 1f;
    public float speed;
    public float health;

    private static float previousspeed;
    private bool originalspeed=true;
    private float originaltimeleft;
    private float originaltime = 6;

    private Transform target;
    public float chaseRange;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    public GameObject particles;
    private float timeLeftBtwSlow; //controla el numero de segundos que quedan para que suelte la habilidad especial 
    public float timeBtwSlow; //tiempo entre habilidad especial 

    private Rigidbody2D rb;
    private Animator anim;
    public bool notInMap = true;

    public GameObject objetos;

    public int value;
    private KeyDrop keyDrop;

    private float deathCont;
    private bool isTouchingWall;
    private float count = 0.2f;

    // Use this for initialization
    void Start()
    {
        keyDrop = GetComponent<KeyDrop>();
        previousspeed = Player.speed;
        timeLeftBtwSlow = timeBtwSlow;
        originaltimeleft = originaltime;

        count = 0.2f;
        deathCont = 1f;
        isTouchingWall = false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        notInMap = true;

        //Movimiento
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // de esta manera nos aseguramos de devolver al jugador su velocidad inicial
        if (originalspeed == false)
        {
            if (originaltimeleft <= 0)
            {
                Player.speed = previousspeed;
                originalspeed = true;
                originaltimeleft = originaltime;
            }
            else
            {
                originaltimeleft -= Time.deltaTime;
            }
        }
       

        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (distance < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            movement();
        }

        //Fin del movimiento

        //Ataque

        //Comprobar si el jugador esta lo suficientemente cerca para atacar
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if (distanceToAttack < attackRange)
        {

            slowdown();


        }
        //Fin del ataque

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




    }

    void movement()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
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
    void slowdown()
    {
            //Comprobar si ha pasado tiempo suficiente desde el ultimo ataque
            if (timeLeftBtwSlow <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
            {
            //Instantiate(particles, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            Player.speed = newspeed; //Quaternion.identity = no rotation
                originalspeed = false;
                timeLeftBtwSlow = timeBtwSlow;
            }
            else
            {
                timeLeftBtwSlow -= Time.deltaTime;
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

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
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
            Player.speed = previousspeed;
            //ObjetsDrop.pos = transform.position;
            //objetos.GetComponent<ObjetsDrop>().Drop();
            keyDrop.SpawnKey();
            // score
            Stats.score = Stats.score + value;

            //Matar al enemigo
            Destroy(gameObject);
        }

    }
    //Fin de muerte del enemigo

   




}
