using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AR_Spawner_Module.Editor;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

public class AbilityCreatorWindowEditor : EditorWindow
{
    private AbilityBaseSO selectedAbilityBase;
    private Vector2 scrollPosition;
    private Color backgroundColor1 = new Color(0.2f, 0.2f, 0.2f);
    private Color backgroundColorDark = new Color(0.15f, 0.15f, 0.15f);
    private Color GreyLight = new Color(0.7f, 0.7f, 0.7f);
    
  
    private Type AbilityBaseType;
    private IEnumerable<Type> allTypes;
    private SerializedObject serializedObject;
    private GUIStyle largeButtonStyle;
    
    
    private string AbilityName;
    const string availableAbilityTypes = "ability Types:";
    const string Create = "Create";
    const string Abilitytype = "AbilityType";
    const string AbilityProperties = "ability Properties:";
    const string selectAAbilityTypeFromTheLeftPanel = "Select a ability Type from the left panel.";
    const string mScript = "m_Script";
    
    
    [MenuItem("Ability/Ability Creator Window")]
    public static void ShowWindow()
    {
        GetWindow<AbilityCreatorWindowEditor>("ability Creator");
    }

    private List<string> buttonLabels = new List<string>();
    
    private void OnEnable()
    {
        Refresh();
        CacheButtonLabels();
        selectedAbilityBase = ScriptableObject.CreateInstance(allTypes.First()) as AbilityBaseSO;
    }
    
    private void CacheButtonLabels()
    {
        buttonLabels.Clear();
        foreach (var type in allTypes)
        {
            string buttonLabel = type.Name.Replace(Abilitytype, "");
            buttonLabels.Add(buttonLabel);
        }
    }


    private void OnGUI()
    {
        // if (inited == false)
        //   return;   

        EditorGUILayout.BeginHorizontal();
        DrawLeftSegment();
        DrawRightSegment();
        EditorGUILayout.EndHorizontal();
        DrawCreateButton();
    }

    
    private void DrawSectionTitleH1(string label)
    {
        if (styleH1 == null)
        {
            styleH1 = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 15, 
                fontStyle = FontStyle.Bold,
                normal = {textColor = GreyLight},
                alignment = TextAnchor.MiddleCenter
            };
        }
        
        GUILayout.Space(10f);
        GUILayout.Label(label, styleH1);
        GUILayout.Space(10f);
    }

    private GUIStyle styleH2;
    private GUIStyle styleH1;
    void DrawSectionTitleH2(string label)
    {
        if (styleH2 == null)
        {
            styleH2 = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14, 
                fontStyle = FontStyle.Bold,
                normal = {textColor = GreyLight}
            };
        }
        GUILayout.Space(10f);
        GUILayout.Label(label, styleH2);
        GUILayout.Space(10f);
    }

    

   

    private void Refresh()
    {
        AbilityBaseType = typeof(AbilityBaseSO);
        allTypes = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => AbilityBaseType.IsAssignableFrom(p) && p != AbilityBaseType);

    }
    
    private void DrawLeftSegment()
    {
        DrawBackgroundVertical(200, backgroundColorDark);
        DrawSectionTitleH1(availableAbilityTypes);
        DrawBackgroundHorizontal(200, backgroundColor1);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.Space(10f);
    
        DrawAvailableAbilityTypes();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }
    
    
    private void DrawAvailableAbilityTypes2()
    {
        foreach (var type in allTypes)
        {
            string buttonLabel = type.Name.Replace(Abilitytype, "");
            if (GUILayout.Button(buttonLabel))
            {
                Debug.Log("AR: 1");
                if (selectedAbilityBase == null || selectedAbilityBase.GetType() != type)
                {
                    Debug.Log("AR: 2");
                    selectedAbilityBase = ScriptableObject.CreateInstance(type) as AbilityBaseSO;
                    AbilityName = buttonLabel;
                    serializedObject = new SerializedObject(selectedAbilityBase);
                }
            }
        }
    }
    private void DrawAvailableAbilityTypes()
    {
        foreach (var buttonLabel in buttonLabels)
        {
            if (GUILayout.Button(buttonLabel))
            {
                Type type = allTypes.First(t => t.Name.Replace(Abilitytype, "") == buttonLabel);
                selectedAbilityBase = ScriptableObject.CreateInstance(type) as AbilityBaseSO;
                AbilityName = buttonLabel;
                serializedObject = new SerializedObject(selectedAbilityBase);
            }
        }
    }
    
    private void DrawRightSegment()
    {
        DrawBackgroundVertical();

        if (selectedAbilityBase != null)
        {
            DrawBackgroundVertical(0, backgroundColorDark);
            DrawSectionTitleH1(selectedAbilityBase.GetType().ToString());
            EditorGUILayout.EndVertical();
            GUILayout.Space(10f);
        }

        if (selectedAbilityBase != null)
        {
            // Pozwól użytkownikowi na edycję AbilityName
            AbilityName = EditorGUILayout.TextField("ability name:", AbilityName);

            // Wyświetl pola dla wybranego obiektu selectedAbilityBase.
            //serializedObject = new SerializedObject(selectedAbilityBase);
            if (serializedObject== null)
                serializedObject = new SerializedObject(selectedAbilityBase);
            
            serializedObject.Update();

            var property = serializedObject.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                if (property.name != mScript) // Pomijamy pole "Script"
                    EditorGUILayout.PropertyField(property, true);
            
                enterChildren = false;
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            if (selectedAbilityBase == null)
                selectedAbilityBase = ScriptableObject.CreateInstance(allTypes.First()) as AbilityBaseSO;
            else
            {
                GUILayout.Label(selectAAbilityTypeFromTheLeftPanel, EditorStyles.wordWrappedLabel);
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawBackgroundVertical(float width = 0, Color _backgroundColor1 = default)
    {
        if (width == 0)
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(_backgroundColor1));
        }
        else
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(_backgroundColor1), (GUILayout.Width(width)));
        }
    }

    private void DrawBackgroundHorizontal(float width = 0, Color _backgroundColor1 = default)
    {
        if (width == 0)
        {
            EditorGUILayout.BeginHorizontal(BackgroundStyle.Get(_backgroundColor1));
        }
        else
        {
            EditorGUILayout.BeginHorizontal(BackgroundStyle.Get(_backgroundColor1), (GUILayout.Width(width)));
        }
    }
    
    private void DrawCreateButton()
    {
        if (largeButtonStyle == null)
        {
            largeButtonStyle = new GUIStyle(GUI.skin.button);
            largeButtonStyle.fontSize = 2 * largeButtonStyle.fontSize;
        }

        if (GUILayout.Button(Create, largeButtonStyle, GUILayout.Height(40)))
        {
            CreateScriptableObject();
        }
        if (GUILayout.Button("Close", largeButtonStyle, GUILayout.Height(40)))
        {
            Close();
        }
    }

    private void CreateScriptableObject2()
    {
        if (selectedAbilityBase != null)
        {
            if (AbilityName == "")
            {
                AbilityName = Regex.Replace(selectedAbilityBase.GetType().Name, Abilitytype, "");
            }

            var selectedType = selectedAbilityBase.GetType();
            
            string AbilityDataPath = "Assets/SO files/ability/"+AbilityName;
            EditorExtensionsAR.CreatePath(AbilityDataPath);

            // Create AbilityBaseSO
            string AbilityLevelPath = AbilityDataPath + "/Level 0.asset";
            AbilityBaseSO loadedAbility = AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(AbilityLevelPath);

            if (loadedAbility == null)
            {
                AssetDatabase.CreateAsset(serializedObject.targetObject, AbilityLevelPath);
                loadedAbility = AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(AbilityLevelPath);
                
                //Create AbilityData
                AbilityDataSO abilityDataSo = ScriptableObject.CreateInstance<AbilityDataSO>();
                abilityDataSo.abilityName = AbilityName;
                abilityDataSo.levels.Add(loadedAbility);
            
                AssetDatabase.CreateAsset(abilityDataSo, AbilityDataPath + "/"+AbilityName + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log(AbilityName +" ability created. Path: " + AbilityLevelPath);
            }
            else
            {
                Debug.LogError("AR: ability exists. Path: " + AbilityLevelPath);
            }
        }
        else
        {
            Debug.LogWarning("Wybierz ability Type z lewej strony okna, zanim utworzysz plik ScriptableObject.");
        }
    }
    private void CreateScriptableObject()
    {
        serializedObject.ApplyModifiedProperties();

        if (selectedAbilityBase != null)
        {
            if (string.IsNullOrEmpty(AbilityName))
            {
                AbilityName = Regex.Replace(selectedAbilityBase.GetType().Name, Abilitytype, "");
            }

            string abilityDataPath = "Assets/SO files/ability/" + AbilityName;
            EditorExtensionsAR.CreatePath(abilityDataPath);

            string abilityLevelPath = abilityDataPath + "/Level 0.asset";
            AbilityBaseSO loadedAbility = AssetDatabase.LoadAssetAtPath<AbilityBaseSO>(abilityLevelPath);

            if (loadedAbility == null)
            {
                // Utwórz nową instancję ScriptableObject typu wybranego przez użytkownika
                loadedAbility = ScriptableObject.CreateInstance(selectedAbilityBase.GetType()) as AbilityBaseSO;
                loadedAbility = selectedAbilityBase;
                AssetDatabase.CreateAsset(loadedAbility, abilityLevelPath);

                //Create AbilityData
                AbilityDataSO abilityDataSo = ScriptableObject.CreateInstance<AbilityDataSO>();
                abilityDataSo.abilityName = AbilityName;
                abilityDataSo.levels.Add(loadedAbility);

                AssetDatabase.CreateAsset(abilityDataSo, abilityDataPath + "/"+AbilityName + " data.asset");

                // Dalsze operacje...
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log(AbilityName + " ability created. Path: " + abilityDataPath);
            }
            else
            {
                Debug.LogError("ability already exists. Path: " + abilityLevelPath);
            }
        }
        else
        {
            Debug.LogWarning("Select an ability Type from the left panel before creating a ScriptableObject.");
        }
    }
}

