using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGlue : MonoBehaviour
{
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 4f;
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
        if(collision.gameObject.tag == "Player")
        {
            Player.playerHealth = Player.playerHealth - Stats.fireSlimeGlueDamage;
        }
    }
}
