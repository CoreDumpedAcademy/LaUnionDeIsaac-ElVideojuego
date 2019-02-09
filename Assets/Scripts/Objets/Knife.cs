using UnityEngine;

public class Knife : MonoBehaviour
{
    // base damage is 10
    public float damageUp;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.damage += damageUp;
            Destroy(gameObject);
        }
    }
}
