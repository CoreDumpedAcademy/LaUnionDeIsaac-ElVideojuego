using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGlue : MonoBehaviour
{

    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 4f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.slowdown == false)
            {
                StartCoroutine(Player.SlowDownSlime());
                Debug.Log("Ha chocado");
                
            }
        }


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
}
