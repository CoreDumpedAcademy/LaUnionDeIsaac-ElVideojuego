using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA2 : MonoBehaviour
{

    public GameObject lavaColumn;

    private float speed = 10;
    private Transform player;
    private Vector3 target;
    private Vector3 normalizeDirection;
    private float fireballDamage;
    private bool explode;
    private float explodeTime;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.position;
        normalizeDirection = (player.position - transform.position).normalized;
        explode = false;
        explodeTime = 1;
        //    En caso de que queramos que el proyectil no siga al jugador sino que se lance hacia su dirección, si no: player.position.x && player.position.y
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("exploded" + explode);
        explodeTime -= Time.deltaTime;
        fireballDamage = Stats.fireballDamage;
        
        if(explode == true)
        {
            for(int i = 0;i < 25; i++)
            {
                Vector2 newPos1 = new Vector2(transform.position.x, transform.position.y + i);
                Instantiate(lavaColumn, newPos1, Quaternion.identity);

                Vector2 newPos2 = new Vector2(transform.position.x, transform.position.y - i);
                Instantiate(lavaColumn, newPos2, Quaternion.identity);

                Vector2 newPos3 = new Vector2(transform.position.x + i, transform.position.y);
                Instantiate(lavaColumn, newPos3, Quaternion.identity);

                Vector2 newPos4 = new Vector2(transform.position.x - i, transform.position.y);
                Instantiate(lavaColumn, newPos4, Quaternion.identity);

                Vector2 newPos5 = new Vector2(transform.position.x + i, transform.position.y + i);
                Instantiate(lavaColumn, newPos5, Quaternion.identity);

                Vector2 newPos6 = new Vector2(transform.position.x - i, transform.position.y - i);
                Instantiate(lavaColumn, newPos6, Quaternion.identity);

                Vector2 newPos7 = new Vector2(transform.position.x + i, transform.position.y - i);
                Instantiate(lavaColumn, newPos7, Quaternion.identity);

                Vector2 newPos8 = new Vector2(transform.position.x - i, transform.position.y + i);
                Instantiate(lavaColumn, newPos8, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else
        {
            transform.position += normalizeDirection * speed * Time.deltaTime;

            if(transform.position == target || explodeTime <= 0)
            {
                explode = true;
            }
        }

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
            explode = true;
        }

        if (collision.gameObject.tag == "Wall")
        {
            explode = true;
            
        }
    }
}
