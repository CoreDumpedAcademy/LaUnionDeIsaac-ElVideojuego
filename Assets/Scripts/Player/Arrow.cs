using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int selector;
    public float lifeTime = 1f;
    public float arrowSpeed = 0.5f;
    public float counter = Stats.cadencia;
    private Rigidbody2D rb;
    private AudioSource AS;

    // Use this for initialization
    void Start ()
    {
        AS = GetComponent<AudioSource>();

        AS.Play();

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
	void Update ()
    {
        //darle un vector de velocidad a la flecha
        if (selector == 1)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.up * Time.deltaTime * arrowSpeed);
                counter = Stats.cadencia;
            }
        }
        else if (selector == 2)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.down * Time.deltaTime * arrowSpeed);
                counter = Stats.cadencia;
            }
        }
        else if (selector == 3)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * arrowSpeed);
                counter = Stats.cadencia;
            }
        }
        else if (selector == 4)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                transform.Translate(Vector3.left * Time.deltaTime * arrowSpeed);
                counter = Stats.cadencia;
            }
        }
        
        //darle un tiempo de vida a la flecha
        lifeTime -= Time.deltaTime;
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
