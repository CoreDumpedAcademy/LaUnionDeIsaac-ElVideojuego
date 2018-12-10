using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetRequest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getRanking()
    {
        StartCoroutine(GetRank());
    }

    IEnumerator GetRank()
    {
        string httpRequest = "http://isaactype.herokuapp.com/score";
        using (UnityWebRequest www = UnityWebRequest.Get(httpRequest))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonString = ("{\"Items\":" + www.downloadHandler.text + "}");
                Rank[] player = JsonHelper.FromJson<Rank>(jsonString);
                

                RellenarLista.RellenarUser(player[0].name + "\n" + player[1].name + "\n" +
                                           player[2].name + "\n" );

                /*+ player[3].nickname + "\n" +
                                           player[4].nickname + "\n" + player[5].nickname + "\n" +
                                           player[6].nickname + "\n" + player[7].nickname + "\n" +
                                           player[8].nickname + "\n" + player[9].nickname + "\n"*/

                RellenarLista.RellenarScore(player[0].score + "\n" + player[1].score + "\n" +
                                            player[2].score + "\n" );

                /*+ player[3].maxScore + "\n" +
                                            player[4].maxScore + "\n" + player[5].maxScore + "\n" +
                                            player[6].maxScore + "\n" + player[7].maxScore + "\n" +
                                            player[8].maxScore + "\n" + player[9].maxScore + "\n"*/


                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}


[System.Serializable]
public class Rank
{
    public int score;
    public string name;
}

