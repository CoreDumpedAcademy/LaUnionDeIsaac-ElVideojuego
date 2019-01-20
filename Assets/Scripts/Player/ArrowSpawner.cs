using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour {

    public GameObject arrowRight;
    public GameObject arrowLeft;
    public GameObject arrowUp;
    public GameObject arrowDown;
    public float shotSpeed;
    public static int arrowDirection;

    public GameObject player;

	// Use this for initialization
	void Start () {

        shotSpeed = 0;

	}
	
	// Update is called once per frame
	void Update () {

        //Darle un tiempo de disparo al jugador
        shotSpeed = shotSpeed - Time.deltaTime;

        
        //Elegir la flecha que se quiere instanciar
        if (shotSpeed <= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.127f, 0);
                Spawn(arrowUp);
                arrowDirection = 1;
                shotSpeed = 0.5f;
            }
            else

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.127f, 0);
                Spawn(arrowDown);
                arrowDirection = 2;
                shotSpeed = 0.5f;
            }
            else

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector3(player.transform.position.x + 0.127f, player.transform.position.y, 0);
                Spawn(arrowRight);
                arrowDirection = 3;
                shotSpeed = 0.5f;
            }
            else

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(player.transform.position.x - 0.127f, player.transform.position.y, 0);
                Spawn(arrowLeft);
                arrowDirection = 4;
                shotSpeed = 0.5f;
            }
            
        }
    }

    //Función que instancia las flechas
    void Spawn(GameObject arrow)
    {
        Instantiate(arrow, transform.position, transform.rotation);
    }
}
