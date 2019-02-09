using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour {

    public GameObject obj;
    public GameObject[] gos;
    public GameObject key;


    public void Start()
    {
        gos = GameObject.FindGameObjectsWithTag("objetos");
    }
    public void SpawnKey()
    {
        obj = Instantiate(key,transform.position, Quaternion.identity);
        obj.transform.SetParent(gos[0].transform);
    }

}
