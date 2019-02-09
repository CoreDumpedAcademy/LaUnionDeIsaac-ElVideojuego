using UnityEngine;

public class Mando : MonoBehaviour
{
    // base slowdown is 0
    public float slowdownUp;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.slowdown -= slowdownUp;
            Destroy(gameObject);
        }
    }
}
