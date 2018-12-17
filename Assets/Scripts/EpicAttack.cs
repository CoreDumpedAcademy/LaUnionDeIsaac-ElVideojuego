using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicAttack : MonoBehaviour {

    private float appearCount = 1f;
    private float deadCont = 2.8f;
    private BoxCollider2D bc;
    public static bool touchingEpicAttack;
    public static float epicAttackDamage;

    // Use this for initialization
    void Start () {
        appearCount = 1f;
        deadCont = 2.8f;
        epicAttackDamage = 50f;
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = !bc.enabled;
        touchingEpicAttack = false;


    }
	
	// Update is called once per frame
	void Update () {
        appearCount -= Time.deltaTime;

        if (appearCount <= 0)
        {
            touchingEpicAttack = true;
            bc.enabled = !bc.enabled;

            deadCont -= Time.deltaTime;
            if (deadCont <= 0)
            {
                touchingEpicAttack = false;
                Destroy(gameObject);
            }

        }
	}
}
