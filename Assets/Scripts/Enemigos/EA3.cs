using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA3 : MonoBehaviour
{
    private float lifeTime;
    private float speed = 4.5f;
    private Transform player;
    private Vector2 target;
    private Vector3 normalizeDirection;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 20f;


    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        normalizeDirection = (player.position - transform.position).normalized;
        transform.position += normalizeDirection * speed * Time.deltaTime;

        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.playerHealth = Player.playerHealth - Stats.holyFire;
        }
    }
}
