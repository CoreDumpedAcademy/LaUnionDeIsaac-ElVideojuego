﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public int numberEnemies = 20;
    public int enemiesSpawned;
    private float x;
    private float y;
    private float z;

    private bool isColliding;
    GameObject[] gos;

    private Vector3 pos;
    public GameObject[] obj;

    public float count = 0.2f;

    // Use this for initialization
    void Start () {
        numberEnemies = 20;
        enemiesSpawned = 0;
        count = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

        
    }

    private void FixedUpdate()
    {
        count = count - Time.deltaTime;
        if (count <= 0)
        {
            EnemySpawn();
        }

        

        Debug.Log(gos.Length);
    }
    //Función que hace aparecer enemigos
    void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);     
    }

    //Función que elige la posición en la que aparecen los enemigos
    public void EnemySpawn()
    {
        while (gos.Length < 20)
        {
            gos = GameObject.FindGameObjectsWithTag("Enemy");

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
                enemiesSpawned++;
            
          
        }
    }
}

