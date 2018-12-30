using UnityEngine;

public class Mascara : MonoBehaviour
{
    // base cooldown is 0.5f
    public float cooldownDown;
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.cooldown -= cooldownDown;
            Destroy(gameObject);
        }
    }
}
