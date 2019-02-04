using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGlue : MonoBehaviour
{

    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime = lifeTime - Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
