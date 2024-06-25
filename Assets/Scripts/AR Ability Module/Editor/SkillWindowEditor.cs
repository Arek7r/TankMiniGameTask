using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

public partial class AbilityWindowEditor : EditorWindow
{
    private AbilityDataSO[] AbilityDataObjects;
    private AbilityDataSO selectedAbilityDataSo;
    private AbilityBaseSO[] selectedAbilityLevels;
 
    private Vector2 scrollPosition;
    private Vector2 scrollPositionLevels;
    private Vector2 scrollPositionAbility;
    
    private float firstColumnWidth = 150f;
    private float secondColumnWidth = 100f;
    
    private string searchAbilityData = "";
    private int selectedAbilityDataIndex = 0;
    private int selectedAbilityLevelIndex = 0;
    
    const string StringAbility = "Ability";
    const string StringLevels = "Levels";
    const string StringProperties = "Properties";
    const string StringMScript = "m_Script";
    const string StringTAbilitydata = "t:AbilityDataSO";

    private SerializedObject serializedObject;
    
    [MenuItem("Ability/Ability Window %q")]
    public static void ShowWindow()
    {
        AbilityWindowEditor window = GetWindow<AbilityWindowEditor>("Ability Window");
        window.minSize = new Vector2(800f, 400f); // Set the initial window size
    }

    private void OnEnable()
    {
        RefreshAbilityData();
        SelectDefaultObjects();
        //
        // if (!hasOpened)
        // {
        //     hasOpened = true;
        //     SelectDefaultObjects();
        // }
    }
    
    private void OnGUI()
    {
        DrawSearchSegment();
        DrawAbilitySegment();
        DrawLevelsSegment();
        DrawProperties();
    }
   
    private void DrawAbilitySegment()
    {
        EditorGUILayout.BeginHorizontal();
        DrawBackground(firstColumnWidth);
        DrawSectionTitle(StringAbility);

        scrollPositionAbility = EditorGUILayout.BeginScrollView(scrollPositionAbility, GUILayout.ExpandHeight(true));

        if (CheckIf_DrawAbilitySegment())
        {
            selectedAbilityDataIndex = GUILayout.SelectionGrid(selectedAbilityDataIndex,
                AbilityDataObjects.Select(sd => sd.abilityName).ToArray(), 1, EditorStyles.miniButton);

            if (selectedAbilityDataIndex < AbilityDataObjects.Length)
            {
                selectedAbilityDataSo = AbilityDataObjects[selectedAbilityDataIndex];
                selectedAbilityLevels = selectedAbilityDataSo.levels.ToArray();
            }
            else
            {
                selectedAbilityDataSo = null;
                selectedAbilityLevels = new AbilityBaseSO[0];
            }
        }
        

        EditorGUILayout.EndScrollView();
        
       
        
        if (GUILayout.Button("Ping file", GUILayout.Height(20)))
        {
            PingSelectedAbility();
        }
        
        if (GUILayout.Button("Refresh", GUILayout.Height(20)))
        {
            RefreshAbilityData();
        }
        
        if (GUILayout.Button("Create ability", GUILayout.Height(30)))
        {
            AbilityCreatorWindowEditor.ShowWindow();
        }
        
        EditorGUILayout.EndVertical();
    }



    private void DrawLevelsSegment()
    {
        GUILayout.Space(5f);
        DrawBackground(secondColumnWidth);
        DrawSectionTitle(StringLevels);

        scrollPositionLevels = EditorGUILayout.BeginScrollView(scrollPositionLevels, GUILayout.ExpandHeight(true));

        if (CheckIf_DrawLevelsSegment())
        {
            selectedAbilityLevelIndex = GUILayout.SelectionGrid(
                selectedAbilityLevelIndex, 
                selectedAbilityLevels.Select(
                    AbilityLevel => AbilityLevel?.name ?? "No name Ability").ToArray(),
                1, 
                EditorStyles.miniButton
            );

            if (selectedAbilityLevelIndex >= selectedAbilityLevels.Length)
                selectedAbilityLevelIndex = 0;

        }
       
        EditorGUILayout.EndScrollView();
        
        if (GUILayout.Button("Add level", GUILayout.Height(30)))
        {
            CreateNewLevel(selectedAbilityDataSo, selectedAbilityDataSo.levels.Last());
        }
        
        EditorGUILayout.EndVertical();
    }

    private void DrawProperties()
    {
        GUILayout.Space(5f);
        DrawBackground();
        DrawSectionTitle(StringProperties);

        if (selectedAbilityLevelIndex >= 0 && selectedAbilityLevelIndex < selectedAbilityLevels.Length)
        {
            if (selectedAbilityLevels[0] != null)
            {
                serializedObject = new SerializedObject(selectedAbilityLevels[selectedAbilityLevelIndex]);
                serializedObject.Update();

                var property = serializedObject.GetIterator();
                bool enterChildren = true;

                while (property.NextVisible(enterChildren))
                {
                    if (property.name != StringMScript) 
                        EditorGUILayout.PropertyField(property, true);
                
                    enterChildren = false;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
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
            selectedAbilityLevels = new AbilityBaseSO[0];
        }

        Repaint();
    }

    private AbilityDataSO[] FindAbilityData()
    {
        string[] guids = AssetDatabase.FindAssets(StringTAbilitydata);
        List<AbilityDataSO> data = new List<AbilityDataSO>();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            AbilityDataSO abilityDataSo = AssetDatabase.LoadAssetAtPath<AbilityDataSO>(assetPath);
            if (abilityDataSo != null && abilityDataSo.abilityName.ToLower().Contains(searchAbilityData.ToLower()))
            {
                data.Add(abilityDataSo);
            }
        }
        return data.ToArray();
    }

    private void PingSelectedAbility()
    {
        if (selectedAbilityDataSo != null)
        {
            EditorGUIUtility.PingObject(selectedAbilityDataSo);
        }
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
    #region CheckIf
    private bool CheckIf_DrawAbilitySegment()
    {
        if (AbilityDataObjects == null)
            return false;

        if (AbilityDataObjects.Length == 0)
            return false;

        return true;
    }
    private bool CheckIf_DrawLevelsSegment()
    {
        if (selectedAbilityLevels == null)
            return false;
        
        if (selectedAbilityLevels.Length == 0)
            return false;
        
        return true;
    }

    #endregion

}
