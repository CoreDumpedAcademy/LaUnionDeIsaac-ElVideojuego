using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public bool grounded = false;
    public AudioSource AS;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!grounded)
        {
            GameObject[] suelos;
            suelos = GameObject.FindGameObjectsWithTag("Floor");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in suelos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            if (distance > 1)
            {
                transform.position = closest.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AS.Play();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }
}
