using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemAR : MonoBehaviour
{
    #region JSON

    // Example:
    // Save:
    //      string filePath = "ścieżka/do/pliku.json";
    //      PlayerProgressSO playerProgress = new PlayerProgressSO();
    //      JsonDataHandler.SaveToJson(playerProgress, filePath);
    // Load:
    //     PlayerProgressSO loadedData = JsonDataHandler.LoadFromJson<PlayerProgressSO>(filePath);
    
    public static void SaveToJson<T>(T data, string filePath)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public static T LoadFromJson<T>(string filePath) where T : class
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(jsonData);
        }
        return null;
    }
    
    #endregion

    #region Prefs

    public static void SaveProgressPrefs<T>(T data,string saveName)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveName, jsonData);
        PlayerPrefs.Save();
    }

    public static T LoadProgressPrefs<T>(string saveName) where T : class
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            string jsonData = PlayerPrefs.GetString(saveName);
            return JsonUtility.FromJson<T>(jsonData);
        }

        return null; 
    }
    

    #endregion
}
