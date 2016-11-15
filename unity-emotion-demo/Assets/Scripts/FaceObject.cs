using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// A C# representation of the Face API from Microsoft Cognitive Services
/// Written by Livi Erickson (@missLiviRose)
/// </summary>
[System.Serializable]
public class FaceObject
{
    public string faceRectangle { get; private set; }
    public List<Emotion> emotions { get; private set; }

    public FaceObject(string rect, string scorelist)
    {
        faceRectangle = rect;
        emotions = ConvertScoresToEmotionDictionary(scorelist);
        Debug.Log("Highest Emotion: " + GetHighestWeighedEmotion().ToString());

    }
    /// <summary>
    /// Convert a JSON-formatted string from the Emotion API call into a List of Emotions
    /// </summary>
    /// <param name="scores"></param>
    /// <returns></returns>
    public List<Emotion> ConvertScoresToEmotionDictionary(string scores)
    {
        List<Emotion> emotes = new List<Emotion>();
        JSONObject _scoresJSON = new JSONObject(scores);
        for(int i = 0; i < _scoresJSON.Count; i++)
        {
            Emotion e = new Emotion(_scoresJSON.keys[i], float.Parse(_scoresJSON.list[i].ToString()));
            emotes.Add(e);
        }
        return emotes;
    }

    /// <summary>
    /// Get the highest scored emotion 
    /// </summary>
    /// <returns></returns>
    public Emotion GetHighestWeighedEmotion()
    {
        Emotion max = emotions[0];
        foreach(Emotion e in emotions)
        {
            if(e.value > max.value)
            {
                max = e;
            }
        }
        return max;
    }
}

/// <summary>
///  A helper class for storing an emotion
///  From the spec of the Cognitive Services API
/// </summary>
public class Emotion
{
    public string name { get; private set; }
    public float value { get; private set; }

    public Emotion(string name, float value)
    {
        this.name = name;
        this.value = value;
    }

    override public string ToString()
    {
        return name + " : " + value;
    }
}

