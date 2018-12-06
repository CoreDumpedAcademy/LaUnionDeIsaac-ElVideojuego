using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour {

    public float speed;
    public float health;
    public float damage;

    public Transform target;
    public float chaseRange;

    public float attackRange;
    private float lastAttackTime;
    public float attackDelay;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Chaising
        //Get the distance to the target and check if the distance is enough to chase the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(distanceToTarget < chaseRange)
        {
            //Start chasing the target
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);

            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        //End of chaising

        //Attack

        //Check if the player is close to attack
        float distanceToAttack = Vector3.Distance(transform.position, target.position);
        if(distanceToAttack < attackRange)
        {
            //Check to see if enoght time has passed since the last attack
            if(Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeDamage", damage);
                //Record the time we attacked
                lastAttackTime = Time.time;
            }
            
        }
 
        //End of Attack
    }
   
    //Demon dead
    public void TakeDamage(int damage)
    {
        //substract the life of the enemy
        health -= damage;
        if(health <= 0)
        {
            //kill the enemy
            Destroy(this.gameObject);
            //Drop a random item
        }

    }
    //End of demon dead
}
