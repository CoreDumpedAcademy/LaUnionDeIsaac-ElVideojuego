using UnityEngine;

public class Mando : MonoBehaviour
{
    // base slowdown is 0
    public float slowdownUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.slowdown -= slowdownUp;
            Destroy(gameObject);
        }
    }
}
