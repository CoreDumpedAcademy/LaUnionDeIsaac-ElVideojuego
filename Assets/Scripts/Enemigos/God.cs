using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    private float timeLeftBtwShots; //controla el numero de segundos que quedan para que dispare
    public float timeBtwShots; //tiempo entre disparo y disparo
    public GameObject projectile;
    private Transform target;

    public GameObject EA2;
    private float EPICtimeLeftBtwShots; //controla el numero de segundos que quedan para que dispare
    public float EPICtimeBtwShots;

    public GameObject EA3;
    private float EPICtimeLeftBtwShots2; //controla el numero de segundos que quedan para que dispare
    public float EPICtimeBtwShots2;

    public float health = 1000;
    // Use this for initialization
    void Start () {

        EPICtimeLeftBtwShots = EPICtimeBtwShots;
        EPICtimeLeftBtwShots2 = 2;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeLeftBtwShots = timeBtwShots;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (EPICtimeLeftBtwShots <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
        {
            Instantiate(EA2, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            EPICtimeLeftBtwShots = EPICtimeBtwShots;
            Debug.Log("EA2");
        }
        else
        {
            EPICtimeLeftBtwShots -= Time.deltaTime;
        }

        if (EPICtimeLeftBtwShots2 <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
        {
            Instantiate(EA3, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            EPICtimeLeftBtwShots2 = EPICtimeBtwShots2;
            
        }
        else
        {
            EPICtimeLeftBtwShots2 -= Time.deltaTime;
        }

        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (timeLeftBtwShots <= 0) //nos aseguramos de que el enemigo lance bolas cada timeBtwShots segundos
        {
            Instantiate(projectile, transform.position, Quaternion.identity); //Quaternion.identity = no rotation 
            timeLeftBtwShots = timeBtwShots;
        }
        else
        {
            timeLeftBtwShots -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            health = health - Stats.damage;
        }
    }
}
