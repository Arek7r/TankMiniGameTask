using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AR_Spawner_Module.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public partial class AbilityWindowEditor : EditorWindow
{
    private Color backgroundColor1 = new Color(0.2f, 0.2f, 0.2f);

    private void DrawSearchSegment()
    {
        GUILayout.Label("Search ability Data:", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
            
        searchAbilityData = EditorGUILayout.TextField(searchAbilityData);
        if (GUILayout.Button("Search", GUILayout.Width(60)) || (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
        {
            RefreshAbilityData();
        }
        
        EditorGUILayout.EndHorizontal();
    }

    private void DrawBackground(float with = 0)
    {
        if (with == 0)
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(backgroundColor1));
        }
        else
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(backgroundColor1), (GUILayout.Width(with)));
        }
    }

    // public void CreateNewLevel(AbilityDataSO abilityDataSo, Type original)
    // {
    //     string AbilityLevelPath = "Assets/SO files/ability/"+ abilityDataSo.abilityName +"/Level " + (abilityDataSo.levels.Count)+".asset";
    //     var  level = ScriptableObject.CreateInstance(original);
    //     AssetDatabase.CreateAsset(level, AbilityLevelPath);
    //     abilityDataSo.levels.Add(AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(AbilityLevelPath));
    // }
    
    public void CreateNewLevel(AbilityDataSO abilityDataSo, ScriptableObject original)
    {
        string AbilityLevelPath = "Assets/SO files/ability/"+ abilityDataSo.abilityName +"/Level " + (abilityDataSo.levels.Count)+".asset";
        var copy = ScriptableObject.CreateInstance(original.GetType());
        
        
        foreach (FieldInfo field in original.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            field.SetValue(copy, field.GetValue(original));
        }
        
        AssetDatabase.CreateAsset(copy, AbilityLevelPath);
        abilityDataSo.levels.Add(AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(AbilityLevelPath));
    }
    
    void DrawSectionTitle(string label)
    {
        GUILayout.Label(label, EditorStyles.boldLabel);
        GUILayout.Space(5f);
    }
}