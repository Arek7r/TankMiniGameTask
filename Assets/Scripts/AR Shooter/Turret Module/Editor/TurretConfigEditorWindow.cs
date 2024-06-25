using System.Collections.Generic;
using System.IO;
using System.Linq;
using _AR_.Extensions;
using AR_Spawner_Module.Editor;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

public partial class TurretConfigEditorWindow : EditorWindow
{
    private TurretGunConfigSO[] AbilityDataObjects;
    private TurretGunConfigSO selectedAbilityDataSo;
    private TurretGunLevel[] selectedAbilityLevels;
    private Vector2 scrollPosition;

    private int selectedAbilityLevelIndex = 0;
    private float firstColumnWidth = 150f;
    private float secondColumnWidth = 100f;
    private int selectedAbilityDataIndex = 0;
    
    
    const string T = "t:";

    [MenuItem("Tools/Turret Config Window %w")]
    public static void ShowWindow()
    {
        TurretConfigEditorWindow window = GetWindow<TurretConfigEditorWindow>("Turret Config");
        window.minSize = new Vector2(800f, 400f); // Set the initial window size
    }

    
    private void OnEnable()
    {
        Refresh();

        // if (!hasOpened)
        // {
        //     hasOpened = true;
        //     SelectDefaultObjects();
        // }
    }
    private void OnGUI()
    {
       // DrawSearchSegment();
        DrawConfigs();
        DrawLevelsSegment();
        DrawProperties();
    }
    private void DrawLevelsSegment()
    {
        GUILayout.Space(5f);
        DrawBackground(secondColumnWidth);
        DrawSectionTitle("Levels");

        if (selectedAbilityDataSo != null && selectedAbilityDataSo.levels != null && selectedAbilityDataSo.levels.Count() > 0)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));

            // Tworzenie napisów dla każdego poziomu
            string[] levelNames = selectedAbilityDataSo.levels
                .Select((level, index) => "Level " + index)
                .ToArray();

            //selectedAbilityLevelIndex = GUILayout.SelectionGrid(selectedAbilityLevelIndex, selectedAbilityLevelIndex.ToString().ToArray(), 1, EditorStyles.miniButton);
            selectedAbilityLevelIndex = GUILayout.SelectionGrid(selectedAbilityLevelIndex,selectedAbilityLevels.Select(lvl => lvl.name).ToArray(), 1, EditorStyles.miniButton);
            
            
            if (selectedAbilityLevelIndex >= selectedAbilityDataSo.levels.Count)
                selectedAbilityLevelIndex = 0;

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Add level", GUILayout.Height(30)))
            {
                CreateLevel();
            }
        }
        else
        {
            CreateLevel();
            //EditorGUILayout.LabelField("No levels available.");
        }

  
        
        EditorGUILayout.EndVertical();
    }
    
    //private SerializedObject serializedObject;
    const string AbilityProperties = "Properties";
    const string mScript = "m_Script";

    private void DrawProperties()
    {
        GUILayout.Space(5f);
        DrawBackground();
        DrawSectionTitle(AbilityProperties);

        // Sprawdzamy, czy mamy wybrany prawidłowy poziom
        if (selectedAbilityDataSo != null && selectedAbilityLevels != null && selectedAbilityLevelIndex >= 0 && selectedAbilityLevelIndex < selectedAbilityLevels.Length)
        {
            SerializedObject serializedLevel = new SerializedObject(selectedAbilityLevels[selectedAbilityLevelIndex]);
            serializedLevel.Update(); // Aktualizujemy SerializedObject

            SerializedProperty property = serializedLevel.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                if (property.name != mScript) // Pomijamy pole "m_Script"
                {
                    EditorGUILayout.PropertyField(property, true);
                }
                enterChildren = false;
            }

            serializedLevel.ApplyModifiedProperties(); // Zastosowanie zmian
        }
        else
        {
            EditorGUILayout.LabelField("No level selected or available.");
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
    
    
    
    // public void CreateNewLevel(TurretGunLevel AbilityDataSo, Type obj)
    // {
    //     string AbilityDataPath = "Assets/SO files/Turret/"+AbilityDataSo.name +"/Level " + (AbilityDataSo.levels.Count)+".asset";
    //     var  AbilityData2 = ScriptableObject.CreateInstance(obj);
    //     AssetDatabase.CreateAsset(AbilityData2, AbilityDataPath );
    //     AbilityDataSo.levels.Add(AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(AbilityDataPath));
    // }
    
    void DrawSectionTitle(string label)
    {
        GUILayout.Label(label, EditorStyles.boldLabel);
        GUILayout.Space(5f);
    }
    
    private void DrawConfigs()
    {
        EditorGUILayout.BeginHorizontal();
        DrawBackground(firstColumnWidth);
        DrawSectionTitle("Configs");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));

        selectedAbilityDataIndex = GUILayout.SelectionGrid(selectedAbilityDataIndex,
            AbilityDataObjects.Select(sd => sd.configName).ToArray(), 1, EditorStyles.miniButton);

        if (selectedAbilityDataIndex < AbilityDataObjects.Length)
        {
            selectedAbilityDataSo = AbilityDataObjects[selectedAbilityDataIndex];
            selectedAbilityLevels = selectedAbilityDataSo.levels.ToArray();
        }
        else
        {
            selectedAbilityDataSo = null;
            selectedAbilityLevels = new TurretGunLevel[0];
        }

        EditorGUILayout.EndScrollView();
        
        if (GUILayout.Button("Create Config", GUILayout.Height(30)))
        {
            CreateConfig();
        }
        
        EditorGUILayout.EndVertical();
    }

    private void CreateConfig()
    {
        string AbilityDataPath = "Assets/SO files/Turret";
        EditorExtensionsAR.CreatePath(AbilityDataPath);
        
        var config = ScriptableObject.CreateInstance<TurretGunConfigSO>();
        EditorExtensionsAR.CreateUniqueAsset(config,AbilityDataPath+"/Turret Config");
        //AssetDatabase.CreateAsset(config,AbilityDataPath+"/Level 0");
        
        TurretGunLevel level = ScriptableObject.CreateInstance<TurretGunLevel>();
        EditorExtensionsAR.CreateUniqueAsset(level,AbilityDataPath+"/Level 0", true);
        //AssetDatabase.CreateAsset(level,AbilityDataPath+"/Turret Config.asset");
        config.configName = "Config";
        config.levels.Add(level);
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Refresh();
    }

    private void CreateLevel()
    {
        string AbilityDataPath = "Assets/SO files/Turret";
        EditorExtensionsAR.CreatePath(AbilityDataPath);
   
        //with file name
        var fullpath = AssetDatabase.GetAssetPath(selectedAbilityDataSo);
        //without file name
        var path = Path.GetDirectoryName(fullpath);
        
        TurretGunLevel level = ScriptableObject.CreateInstance<TurretGunLevel>();
        EditorExtensionsAR.CreateUniqueAsset(level,path+"/Level 0", true);
        selectedAbilityDataSo.levels.Add(level);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Refresh();
    }

    private void Refresh()
    {
        RefreshAbilityData();
        SelectDefaultObjects();
        Repaint();
    }
    private Color backgroundColor1 = new Color(0.2f, 0.2f, 0.2f);

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
    
    private void RefreshAbilityData()
    {
        AbilityDataObjects = FindAbilityData();
        if (AbilityDataObjects.Length > 0)
        {
            selectedAbilityDataSo = AbilityDataObjects[0];
            selectedAbilityLevels = selectedAbilityDataSo.levels.ToArray();
        }
        else
        {
            selectedAbilityDataSo = null;
            selectedAbilityLevels = new TurretGunLevel[0];
        }

        Repaint();
    }
    
    // Function to select the default objects
    private void SelectDefaultObjects()
    {
        if (AbilityDataObjects.Length > 0)
        {
            selectedAbilityDataSo = AbilityDataObjects[0];
            selectedAbilityLevels = selectedAbilityDataSo.levels.ToArray();

            if (selectedAbilityLevels.Length > 0)
            {
                selectedAbilityLevelIndex = 0;
            }
        }
    }
    
    private TurretGunConfigSO[] FindAbilityData()
    {
        string[] guids = AssetDatabase.FindAssets(T+nameof(TurretGunConfigSO));
        List<TurretGunConfigSO> data = new List<TurretGunConfigSO>();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            TurretGunConfigSO dataSO = AssetDatabase.LoadAssetAtPath<TurretGunConfigSO>(assetPath);
            
            if (dataSO != null )
            {
                data.Add(dataSO);
            }
        }
        return data.ToArray();
    }
}
