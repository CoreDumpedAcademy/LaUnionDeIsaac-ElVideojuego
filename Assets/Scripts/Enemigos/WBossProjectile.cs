using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBossProjectile : MonoBehaviour {

    private float speed=10;
    private Transform player;
    private Vector2 target;
    private Vector3 normalizeDirection;
    private float fireballDamage;
    private AudioSource AS;
    // Use this for initialization
    void Start () {
        AS = GetComponent<AudioSource>();

        AS.Play();

        player = GameObject.FindGameObjectWithTag("Player").transform;

         normalizeDirection = (player.position - transform.position).normalized;
        //    En caso de que queramos que el proyectil no siga al jugador sino que se lance hacia su dirección, si no: player.position.x && player.position.y
    }
	
	// Update is called once per frame
	void Update () {

        fireballDamage = Stats.fireballDamage;
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerCollider.hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - fireballDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);

                PlayerCollider.hitCooldown = 1f;
            }

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
