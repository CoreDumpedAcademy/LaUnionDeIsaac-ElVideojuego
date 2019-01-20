using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private float x;
    private float y;
    private float z;
    private Vector3 pos;

    public static Vector3 playerSpawn;

    public float count = 0.2f;
    public float count2 = 1f;

    // Use this for initialization
    void Start() {
        count = 0.1f;
        count2 = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        count = count - Time.deltaTime;
        if (count <= 0)
        {
            x = Random.Range(-74f, 74f);
            y = Random.Range(-74f, 74f);
            z = 0;
            pos = new Vector3(x, y, z);
            Debug.Log(pos);
            transform.position = pos;
            count = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            playerSpawn = transform.position;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            playerSpawn = transform.position;
            Destroy(gameObject);
            count = 0.1f;
        }
    }
}
