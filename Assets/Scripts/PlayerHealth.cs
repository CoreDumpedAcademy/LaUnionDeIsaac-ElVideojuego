using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private float percentageOfHealth;
    RectTransform rt;
    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
        
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //Función que agranda o disminuye la barra de vida en base a la vida del personaje
    public void SetHealth(float playerHealth)
    {
        percentageOfHealth = playerHealth / 200f;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x * percentageOfHealth, rt.sizeDelta.y);
        transform.position = new Vector3(transform.position.x * percentageOfHealth + 0.02f, transform.position.y, transform.position.z);
    }
}
