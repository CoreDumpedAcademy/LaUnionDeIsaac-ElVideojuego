using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFire : MonoBehaviour
{
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 10f;

        
    }

    // Update is called once per frame
    void Update()
    {
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
