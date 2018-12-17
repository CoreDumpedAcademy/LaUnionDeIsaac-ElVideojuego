using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float lifeTime = 1f;
    private Rigidbody2D rb;
    public float arrowSpeed = 0.5f;
    private int selector;
    public float counter = 0.025f;
    public static float arrowDamage;

    // Use this for initialization
    void Start () {

        //Elegir la dirección de la flecha
        if (ArrowSpawner.arrowDirection == 1)
        {
            selector = 1;
        }
        else if (ArrowSpawner.arrowDirection == 2)
        {
            selector = 2;
        }
        else if (ArrowSpawner.arrowDirection == 3)
        {
            selector = 3;
        }
        else if (ArrowSpawner.arrowDirection == 4)
        {
            selector = 4;
        }

    }
	
	// Update is called once per frame
	void Update () {

        //Le damos el daño a la flecha
        arrowDamage = Stats.damage;

        //darle un vector de velocidad a la flecha
        if (selector == 1)
        {
            counter -= Time.deltaTime;

            if (counter < 0)
            {
                transform.Translate(Vector3.up * Time.deltaTime * arrowSpeed);
                counter = 0.025f;

            }
            
        }
        else if (selector == 2)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.down * Time.deltaTime * arrowSpeed);
                counter = 0.025f;

            }
        }
        else if (selector == 3)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * arrowSpeed);
                counter = 0.025f;

            }
        }
        else if (selector == 4)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.left * Time.deltaTime * arrowSpeed);
                counter = 0.025f;

            }
        }


        //darle un tiempo de vida a la flecha
        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
