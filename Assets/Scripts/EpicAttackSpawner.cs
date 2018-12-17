using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicAttackSpawner : MonoBehaviour {

    public GameObject[] obj;
    public float tiempoMin = 1.25f;
    public float tiempoMax = 2.5f;

    Vector3 pos;
    private float x, y, z;



    // Use this for initialization
    void Start()
    {

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

        x = Random.Range(-21f, 21f);
        y = Random.Range(-21f, 21f);
        z = 0;

        pos = new Vector3(x, y, z);

        transform.position = pos;

    }

    void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(tiempoMin, tiempoMax));
    }
}
