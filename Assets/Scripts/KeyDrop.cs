using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour {

    public GameObject key;

    public void SpawnKey()
    {
        Instantiate(key);
    }

}
