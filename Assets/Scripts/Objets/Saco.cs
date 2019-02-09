using UnityEngine;

public class Saco : MonoBehaviour
{
    // base robovida is 0
    public float robovidaUp;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.robovida += robovidaUp;
            Destroy(gameObject);
        }
    }
}
