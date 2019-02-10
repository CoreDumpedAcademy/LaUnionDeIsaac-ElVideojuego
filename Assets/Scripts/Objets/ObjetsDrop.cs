using UnityEngine;

public class ObjetsDrop : MonoBehaviour
{
    public GameObject[] upgrade;
    public GameObject pocionDeVida;
    public GameObject parent;
    public GameObject obj;
    public GameObject[] gos;
    public static Vector3 pos;
	// Use this for initialization
	void Start ()
    {
        gos = GameObject.FindGameObjectsWithTag("objetos");
    }
	
    public void Drop()
    {
        if (Stats.potionDropRate >= Random.value)
        {
            Debug.Log("se supone que hay pocion");
            obj = Instantiate(pocionDeVida);
            obj.transform.SetParent(gos[0].transform);
            obj.transform.position = pos;
            obj.transform.position = new Vector3(pos.x, pos.y + 1);
        }
        if (Stats.dropRate >= Random.value)
        {
            obj = Instantiate(upgrade[Random.Range(0, 7)]);
            obj.transform.SetParent(gos[0].transform);
            obj.transform.position = pos;
        }
    }
}
