using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseEmotionResponse : MonoBehaviour {

    public List<FaceObject> faces {  get; private set; }

	// Use this for initialization
	void Start () {
        faces = new List<FaceObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Parse JSON data from the response
    /// Pass in the response from the API call
    /// </summary>
    /// <param name="respString"></param>
    public void ParseJSONData(string respString)
    {
        JSONObject dataArray = new JSONObject(respString);
        for(int i = 0; i < dataArray.Count; i++)
        {
            faces.Add(ConvertObjectToFaceObject(dataArray[i]));
        }
    }
    /// <summary>
    /// A helper class for converting the JSON object into a Face Object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private FaceObject ConvertObjectToFaceObject(JSONObject obj)
    {
        return new FaceObject(obj.list[0].ToString(), obj.list[1].ToString());
    }
    
}
