using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelProjectile : MonoBehaviour
{

    private float speed = 10;
    private Transform player;
    private Vector2 target;
    private Vector3 normalizeDirection;
    private float angelProjectileDamage;
    public Vector2 target1;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        normalizeDirection = (player.position - transform.position).normalized;

        

        //    En caso de que queramos que el proyectil no siga al jugador sino que se lance hacia su dirección, si no: player.position.x && player.position.y
    }

    // Update is called once per frame
    void Update()
    {

        target1 = Player.playerPos;

        Vector3 direction = target1 - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(normalizeDirection.y, normalizeDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle -180, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);

        angelProjectileDamage = Stats.angelProjectileDamage;
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerCollider.hitCooldown <= 0)
            {
                Player.playerHealth = Player.playerHealth - angelProjectileDamage;
                PlayerPrefs.SetFloat("firstHealth", Player.playerHealth);

                PlayerCollider.hitCooldown = 1f;
            }

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