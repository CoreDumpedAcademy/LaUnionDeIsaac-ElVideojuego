using UnityEngine;

public class Espejo : MonoBehaviour
{
    // base cadencia is 0.025f
    public float cadenciaUP;
    public AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AS.Play();
            Stats.cadencia -= cadenciaUP;
            Destroy(gameObject);
        }
    }
}
