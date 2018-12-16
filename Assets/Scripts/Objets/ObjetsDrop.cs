using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetsDrop : MonoBehaviour {

    public GameObject[] upgrade;
    public GameObject parent;
    public GameObject obj;
    public GameObject[] gos;
	// Use this for initialization
	void Start () {
        gos = GameObject.FindGameObjectsWithTag("objetos");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Drop()
    {
        obj = Instantiate(upgrade[Random.Range(0, 6)]);
        obj.transform.SetParent(gos[0].transform);
    }
}
