using UnityEngine;

public class Donut : MonoBehaviour
{
    // base health is 200
    public float healthUp;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.health += healthUp;
            Destroy(gameObject);
        }
    }
}
