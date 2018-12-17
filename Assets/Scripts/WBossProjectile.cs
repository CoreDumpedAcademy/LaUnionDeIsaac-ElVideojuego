using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBossProjectile : MonoBehaviour {

    private float speed=10;
    public static float fireballDamage;
    private Transform player;
    private Vector2 target;
    private Vector3 normalizeDirection;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

         normalizeDirection = (player.position - transform.position).normalized;
        //    En caso de que queramos que el proyectil no siga al jugador sino que se lance hacia su dirección, si no: player.position.x && player.position.y
    }
	
	// Update is called once per frame
	void Update () {

        fireballDamage = 30f;

        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyProjectile();
        }

        if(collision.gameObject.tag == "Wall")
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
