using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public bool notInMap = true;
    private float count = 0.2f;
    public int floorCounter = 1;
    public int actualFloor;
    Animator anim;


    // Use this for initialization
    void Start () {
        //inicializamos variables y pillamos componentes
        notInMap = true;
        anim = GetComponent<Animator>();
        actualFloor = PlayerPrefs.GetInt("actualFloor");
    }
	
	// Update is called once per frame
	void Update () {

        //detecta si el jugador tiene la llave para cambiar la animación de cerrado a abierto
        if (Player.hasKey)
        {
            anim.SetBool("Unlock", true);
        }
        else
        {
            anim.SetBool("Unlock", false);
        }

        //Si el portal al aparecer no aparece en el mapa, este se borra y se genera otro
        count = count - Time.deltaTime;

        if (count <= 0 && notInMap == true)
        {
            Destroy(gameObject);
        }

    }

    //detecta si está tocando o no el mapa
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            notInMap = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    //permite pasar al siguiente piso si el jugador tiene la llave
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.hasKey)
        {
            if(actualFloor < 8)
            {
                SceneManager.LoadSceneAsync("Floor" + actualFloor);
                floorCounter++;
                PlayerPrefs.SetInt("actualFloor", floorCounter);
            }
            else
            {
                SceneManager.LoadSceneAsync("God'sRoom");
                floorCounter = 0;
            }
            
        }
    }
}
