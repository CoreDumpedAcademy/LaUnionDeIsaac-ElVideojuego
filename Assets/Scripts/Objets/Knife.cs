using UnityEngine;

public class Knife : MonoBehaviour
{
    // base damage is 10
    public float damageUp;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.damage += damageUp;
            Destroy(gameObject);
        }
    }
}
