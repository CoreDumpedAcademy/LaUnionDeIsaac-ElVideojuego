using UnityEngine;

public class Tacon : MonoBehaviour
{
    // base speed is 5
    public float speedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.speed += speedUp;
            Player.speed = Stats.speed;
            Destroy(gameObject);
        }
    }
}
