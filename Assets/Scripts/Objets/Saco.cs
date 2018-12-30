using UnityEngine;

public class Saco : MonoBehaviour
{
    // base robovida is 0
    public float robovidaUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.robovida += robovidaUp;
            Destroy(gameObject);
        }
    }
}
