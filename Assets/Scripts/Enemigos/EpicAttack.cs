using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicAttack : MonoBehaviour
{

    private float appearCount = 1f;
    private float deadCont = 2.8f;
    private float epicAttackDamage;

    // Use this for initialization
    void Start()
    {
        appearCount = 1f;
        deadCont = 2.8f;


    }

    // Update is called once per frame
    void Update()
    {
        epicAttackDamage = Stats.epicAttackDamage;
        appearCount -= Time.deltaTime;

        if (appearCount <= 0)
        {
            
           

            deadCont -= Time.deltaTime;
            if (deadCont <= 0)
            {
                
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.playerHealth = Player.playerHealth - Stats.fireSlimeGlueDamage;
        }
    }
}
