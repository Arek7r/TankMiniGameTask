using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;

#endif

public static class EditorExtensionsAR
{
#if UNITY_EDITOR

    public static  void CreatePath(string path)
    {
        if (!AssetDatabase.IsValidFolder(path))
        {
            string[] pathParts = path.Split('/');
            string currentPath = "";

            foreach (string part in pathParts)
            {
                string fullPath = Path.Combine(currentPath, part);

                if (!AssetDatabase.IsValidFolder(fullPath))
                {
                    AssetDatabase.CreateFolder(currentPath, part);
                }

                currentPath = fullPath;
            }
        }
    }
    
    public static void CreateUniqueAsset<T>(T asset, string basePath, bool startFrom0 = true) where T : Object
    {
        string assetPath = GetUniqueAssetPath(basePath);
        AssetDatabase.CreateAsset(asset, assetPath);
    }

    private static string GetUniqueAssetPath(string basePath)
    {
        string path = basePath + ".asset";
        int counter = 1;

        // Regex do wykrywania liczby na końcu nazwy pliku (np. "Turret Config 2")
        Regex regex = new Regex(@"(\d+)$");

        while (AssetDatabase.LoadAssetAtPath<Object>(path) != null)
        {
            Match match = regex.Match(basePath);
            if (match.Success)
            {
                // Jeśli na końcu jest liczba, inkrementujemy ją
                counter = int.Parse(match.Groups[1].Value) + 1;
                basePath = regex.Replace(basePath, "") + counter;
            }
            else
            {
                // Jeśli na końcu nie ma liczby, po prostu dodajemy licznik
                basePath += " " + counter;
            }

            path = basePath + ".asset";
            counter++;
        }

        return path;
    }
    
#endif

}
