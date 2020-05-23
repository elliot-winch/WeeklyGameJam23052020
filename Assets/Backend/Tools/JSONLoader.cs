using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class JSONLoader
{
    public static T LoadFromFile<T>(string relativeFilePath) where T : class
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, relativeFilePath);

        if (File.Exists(filePath))
        {
            T obj = JsonConvert.DeserializeObject(File.ReadAllText(filePath), typeof(T)) as T;

            if(obj == null)
            {
                Debug.LogError("Available Presentations: Failed to read from file");
            }

            return obj;
        }
        else
        {
            Debug.LogError("Failed to find file at " + relativeFilePath);

            return null;
        }
    }
}
