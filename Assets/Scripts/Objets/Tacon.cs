using UnityEngine;

public class Tacon : MonoBehaviour
{
    // base speed is 5
    public float speedUp;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.speed += speedUp;
            Player.speed = Stats.speed;
            Destroy(gameObject);
        }
    }
}
