using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{

    public float speed;
    public float health;

    private Transform target;
    public float stopDistance;
    public float retreatDistance;

    public bool isAttacking;

    private float timeLeftBtwShots;
    public float timeBtwShots;
    public GameObject projectile;

    private Rigidbody2D rb;
    private Animator anim;
    public bool notInMap = true;
    private float count = 0.2f;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        timeLeftBtwShots = timeBtwShots;
        isAttacking = false;
        notInMap = true;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {

        //Movimiento

        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < stopDistance && (Vector2.Distance(transform.position, target.position) > retreatDistance))
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }

        //Fin del movimiento

        //Animaciones

        if(isAttacking == false)
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
        } else if (isAttacking == true)
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


        if(timeLeftBtwShots <= 0.3f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        //Fin de las animaciones

        //Ataque

        if (timeLeftBtwShots <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
        {
            Instantiate(projectile, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            timeLeftBtwShots = timeBtwShots;
        }
        else
        {
            timeLeftBtwShots -= Time.deltaTime;
        }

        //Fin del ataque

        //muerte si no se encuentra dentro del mapa
        count = count - Time.deltaTime;

        if (count <= 0 && notInMap == true)
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


    public void TakeDamage()
    {
        //Restar vida al enemigo
        health -= 10;
        if (health <= 0)
        {
            //Matar al enemigo
            Destroy(this.gameObject);
        }

    }
    //Fin de muerte del enemigo

}
