using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicGenerator : MonoBehaviour {

    public GameObject[] gos;
    public GameObject WminiBoss;
    public static bool maxEnemies = false;
    public static bool enemySpawned = false;

    private float x;
    private float y;
    private float z;

    private Vector3 pos;
    private float count = 0.2f;

    // Use this for initialization
    void Start () {

        enemySpawned = false;
        maxEnemies = false;
        count = 20f;
    }
	
	// Update is called once per frame
	void Update () {



        count = count - Time.deltaTime;
        if (count <= 0)
        {
            EnemySpawn();
        }

    }

    void Spawn()
    {
        Instantiate(WminiBoss, transform.position, Quaternion.identity);
    }

    void EnemySpawn()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (gos.Length < 11 && maxEnemies == false && enemySpawned == false)
        {
            x = Random.Range(-74f, 74f);
            y = Random.Range(-74f, 74f);
            z = 0;
            pos = new Vector3(x, y, z);
            Debug.Log(pos);
            transform.position = pos;


            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(transform.position), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag != "Wall")
            {
                
                Spawn();
                enemySpawned = true;
                maxEnemies = true;
            }
        }
    }
}
