using UnityEngine;

public class Donut : MonoBehaviour
{
    // base health is 200
    public float healthUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.health += healthUp;
            Destroy(gameObject);
        }
    }
}
