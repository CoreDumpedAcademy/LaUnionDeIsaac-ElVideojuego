using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesLeft : MonoBehaviour
{

    private TextMeshProUGUI enemyCounter;
    private int enemiesLeft = 0;
    public float appearTime;


    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = 0;
        enemyCounter = GetComponent<TextMeshProUGUI>();
        enemyCounter.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        appearTime -= Time.deltaTime;

        if (appearTime <= 0)
        {
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyCounter.text = enemiesLeft.ToString();
        }
        
    }
}
