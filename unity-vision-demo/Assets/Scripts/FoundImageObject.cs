using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// A C# representation of the Face API from Microsoft Cognitive Services
/// Written by Livi Erickson (@missLiviRose)
/// </summary>
[System.Serializable]
public class FoundImageObject
{
    public List<Category> categories { get; private set; }

    public FoundImageObject(JSONObject cat)
    {
        categories = ConvertScoresToCategoryDictionary(cat);
        GetHighestCategory();

    }
    /// <summary>
    /// Convert a JSON-formatted string from the Emotion API call into a List of Emotions
    /// </summary>
    /// <param name="scores"></param>
    /// <returns></returns>
    public List<Category> ConvertScoresToCategoryDictionary(JSONObject cat)
    {
        List<Category> visionGuesses = new List<Category>();
        for(int i = 0; i < cat.Count; i++)
        {
            JSONObject lineObj = cat[i];
            Category c = new Category(lineObj.list[0].ToString(), float.Parse(lineObj.list[1].ToString()));
            visionGuesses.Add(c);
            Debug.Log(c.ToString());
        }
        return visionGuesses;
    }

    /// <summary>
    /// Get the highest scored emotion 
    /// </summary>
    /// <returns></returns>
    public Category GetHighestCategory()
    {
        Category max = categories[0];
        foreach(Category e in categories)
        {
            if(e.score > max.score)
            {
                max = e;
            }
        }
        Debug.Log("Most Recognized Category: " + max.ToString());
        return max;
    }
}

/// <summary>
///  A helper class for storing an emotion
///  From the spec of the Cognitive Services API
/// </summary>
public class Category
{
    public string name { get; private set; }
    public float score { get; private set; }

    public Category(string name, float score)
    {
        this.name = name;
        this.score = score;
    }

    override public string ToString()
    {
        return name + " : " + score;
    }
}

