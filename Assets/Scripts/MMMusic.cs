using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MMMusic");
        if(objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Buttons.stopMMMusic == true)
        {
            Destroy(gameObject);
        }
    }
}
