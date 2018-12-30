using UnityEngine;

public class Espejo : MonoBehaviour
{
    // base cadencia is 0.025f
    public float cadenciaUP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats.cadencia -= cadenciaUP;
            Destroy(gameObject);
        }
    }
}
