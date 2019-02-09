using UnityEngine;

public class Mascara : MonoBehaviour
{
    // base cooldown is 0.5f
    public float cooldownDown;
    public AudioSource AS;
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.cooldown -= cooldownDown;
            Destroy(gameObject);
        }
    }
}
