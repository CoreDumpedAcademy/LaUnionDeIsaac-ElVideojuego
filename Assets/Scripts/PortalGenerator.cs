using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGenerator : MonoBehaviour {

    public int numberPortals = 1;
    public int PortalsSpawned;
    private float x;
    private float y;
    private float z;

    private bool isColliding;
    public GameObject[] gos;

    private Vector3 pos;
    public GameObject[] obj;
    public bool maxPortalsInstantiated = false;

    public float count = 0.2f;
    public float count2 = 1f;

    // Use this for initialization
    void Start()
    {
        numberPortals = 1;
        PortalsSpawned = 0;
        count = 0.1f;
        count2 = 1f;
        maxPortalsInstantiated = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        count = count - Time.deltaTime;
        if (count <= 0)
        {
            PortalSpawn();
        }



        Debug.Log(gos.Length);
    }
    //Función que hace aparecer portales
    void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
    }

    //Función que elige la posición en la que aparecen los portales
    public void PortalSpawn()
    {
        gos = GameObject.FindGameObjectsWithTag("Portal");
        if (gos.Length < numberPortals && maxPortalsInstantiated == false)
        {


            x = Random.Range(-49f, 49f);
            y = Random.Range(-49f, 49f);
            z = 0;
            pos = new Vector3(x, y, z);
            transform.position = pos;


            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(transform.position), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag != "Wall")
            {
                Spawn();

            }
            PortalsSpawned++;


        }
        else
        {
            
            maxPortalsInstantiated = true;
            
        }
    }
}
