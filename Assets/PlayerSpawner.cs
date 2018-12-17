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

    }

    // Update is called once per frame
    void Update()
    {




        count = count - Time.deltaTime;
        if (count <= 0)
        {
            PlayerMove();
        }
    }

    //Función que elige la posición en la que aparece el jugador
    public void PlayerMove()
    {
        


        x = Random.Range(-74f, 74f);
        y = Random.Range(-74f, 74f);
        z = 0;
        pos = new Vector3(x, y, z);
        transform.position = pos;

        playerSpawn = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(playerSpawn), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.tag != "Wall")
        {
            Debug.Log("Epicidad 1");
            Destroy(gameObject);
        }
            


        

    }
}
