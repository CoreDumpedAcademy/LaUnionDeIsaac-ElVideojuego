using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesLeft : MonoBehaviour
{

    private TextMeshProUGUI enemyCounter;
    private int enemiesLeft = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = 0;
        enemyCounter = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Player.appearTime <= 0)
        {
            gameObject.SetActive(true);
        }
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCounter.text = enemiesLeft.ToString();
    }
}
