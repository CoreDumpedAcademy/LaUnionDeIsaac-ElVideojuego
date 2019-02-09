using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomoParticles : MonoBehaviour
{

    private float speed = 10;
    private Transform player;
    private Vector2 target;
    private Vector3 normalizeDirection;
    private AudioSource AS;
    private float lifeTime;
    // Use this for initialization
    void Start()
    {
        lifeTime = 3f;
       // AS = GetComponent<AudioSource>();

       // AS.Play();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        normalizeDirection = (player.position - transform.position).normalized;
        //    En caso de que queramos que el proyectil no siga al jugador sino que se lance hacia su dirección, si no: player.position.x && player.position.y
    }

    // Update is called once per frame
    void Update()

    {
        transform.position += normalizeDirection * speed * Time.deltaTime;

        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            DestroyProjectile();
        }

        if (collision.gameObject.tag == "Wall")
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
