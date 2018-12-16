using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicGenerator : MonoBehaviour {

    public GameObject[] gos;
    public GameObject WminiBoss;
    public static bool maxEnemies=false;

    private float x;
    private float y;
    private float z;

    private Vector3 pos;
    private float count = 0.2f;

    // Use this for initialization
    void Start () {
        maxEnemies = false;
        count = 2f;
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
        if (gos.Length < 11 && maxEnemies == false)
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
                maxEnemies = true;
            }
        }
    }
}
