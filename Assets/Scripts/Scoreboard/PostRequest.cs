using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRequest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PostRequests()
    {
        Debug.Log("ejecutada la request");
        string url = "http://isaactype.herokuapp.com/score";

        WWWForm formDate = new WWWForm();
        formDate.AddField("name", UserValues.GetName());
        formDate.AddField("score", UserValues.GetScore());

        WWW www = new WWW(url, formDate);


        StartCoroutine(Requests(www));
    }

    // Update is called once per frame
    IEnumerator Requests(WWW www)
    {
        yield return www;

        Debug.Log(www.text);

    }
}
