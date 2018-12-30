using UnityEngine;

public class ObjetsDrop : MonoBehaviour
{
    public GameObject[] upgrade;
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
        obj = Instantiate(upgrade[Random.Range(0, 6)]);
        obj.transform.SetParent(gos[0].transform);
        obj.transform.position = pos;
    }
}
