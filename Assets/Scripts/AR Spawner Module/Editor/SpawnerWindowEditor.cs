using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AR_Spawner_Module.Editor
{
    public partial class SpawnerWindowEditor : EditorWindow
    {
        #region Variables
        private static SpawnerWindowEditor currentWindow;
        private WaveData selectedWave;
        private WaveData previousSelectedWave;
        private SpawnerData selectedSpawnerData;

        private int selectedWaveIndex = 0;
        private int selectedTabIndex = 0;
        private string[] waveOptions;
        
        private Vector2 scrollPositionWaveSegment;
        private Vector2 scrollPositionPropertiesSegment;
        private Vector2 scrollPositionObjectsSegment;

        
        private int widthWaveSegment =200;
        private int widthPropertiesSegment = 250;
        private int widthObjectsSegment = 350;
        private int widthObjectsHelperSegment = 200;
        
        private bool isStyleInitialized = false;
        #region Visuals
        private Color backgroundSegment = new Color(0.2f, 0.2f, 0.2f);
        private Color backgroundColumns = new Color(0.2f, 0.2f, 0.2f);
        private Color backgroundObjectsListItem = new Color(0.15f, 0.15f, 0.15f);
        private Color backgroundTitle1 = new Color(0.1f, 0.1f, 0.1f);
        private Color greyLight = new Color(0.7f, 0.7f, 0.7f);
        private Color white = new Color(0.5f, 0.5f, 0.5f);
        
        private GUIStyle styleH1;
        private GUIStyle styleH2;
        private GUIStyle largeLabelStyle;
        #endregion
      
        //  Settings tab
        private Vector2 scrollPositionSpawnDataSegment;
        private List<SpawnerData> spawnerDataList = new List<SpawnerData>();
        private int selectedSpawnerDataIndex = -1;
        private int widthDataListSegment = 500;
       
        #region Strings
        const string String_Waves = "Waves";
        const string String_Properties = "Properties";
        const string String_Objects = "Objects";
        const string String_ObjectsHelper = "Objects Helper";
        const string String_Remove = "Remove";
        const string String_Prefab = "Prefab";
        const string String_SpawnChange = "SpawnChange";
        const string String_SpawnerData = "SpawnerData";
        const string String_WaveCreator = "Wave Creator";
        const string String_None = "None";
        const string String_Main = "Main";
        const string String_Settings = "Settings";
        const string String_Refresh = "Refresh";
        const string String_RemoveLastWave = "Remove last Wave";
        const string String_CreateWave = "Create Wave";
        const string String_WaveName = "Wave name";
        const string String_AddNewObjectToSpawn = "Add New Object to Spawn";
        const string String_RemoveLastObject = "Remove Last Object";
        const string String_ClearList = "Clear List";
        const string String_CopyListFromPreviousWave = "Copy list from previous wave";
        const string String_TSpawnerdata = "t:SpawnerData";
        const string String_AutoStartNextWave = "Auto start next wave";
        const string String_SpawnStartDelayS = "Spawn Start Delay (s)";
        const string String_SpawnEveryS = "Spawn Every (s)";
        const string String_WaveEndCondition = "Wave End Condition";
        #endregion

        
        #endregion

        [MenuItem("Spawner/Wave Window %e")]
        public static void ShowWindow()
        {
            if (currentWindow != null)
            {
                currentWindow.Close();
                BackgroundStyle.ResetBackgroundStyle();
            }

            currentWindow = GetWindow<SpawnerWindowEditor>(String_WaveCreator);
            currentWindow.minSize = new Vector2(800f, 400f); // Set the initial window size
        }

        private void OnEnable()
        {
            EditorApplication.delayCall += InitWindow;
        }

        private void InitWindow()
        {
            RefreshSpawnerDataList();
            RefreshData();
            RefreshWaveList();
            CreateLabelStyle();
            
            Selection.selectionChanged -= OnSelectionChange;
            Selection.selectionChanged += OnSelectionChange;
            isStyleInitialized = true;
        }
       

        private void OnDisable()
        {
            EditorApplication.delayCall -= CreateLabelStyle;
            Selection.selectionChanged -= OnSelectionChange;
            
            if (selectedSpawnerData != null)
                SaveSelectedSpawnerData();
            
            BackgroundStyle.ResetBackgroundStyle();
            currentWindow = null;
        }

        private void OnSelectionChange()
        {
            if (isStyleInitialized == false)
                return;
            
            RefreshData();
            RefreshWaveList();
        }
        
        private void OnGUI()
        {
            if (isStyleInitialized == false)
                return;

            DrawEditingLabel();
            DrawTabBar();
            
            switch (selectedTabIndex)
            {
                case 0:
                    DrawMainTab();
                    break;
                case 1:
                    RefreshSpawnerDataList();
                    DrawSettingsTab();
                    break;
            }
        }

        #region Top
        private void DrawEditingLabel()
        {
            string editingText = "";

            if (selectedSpawnerData != null)
            {
                // Dodaj nazwę pliku lub obiektu
                editingText += selectedSpawnerData.name;
            }
            else
            {
                editingText += String_None;
            }

            GUILayout.Label(editingText, largeLabelStyle);
        }

        private void DrawTabBar()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Toggle(selectedTabIndex == 0, String_Main, EditorStyles.toolbarButton))
            {
                selectedTabIndex = 0;
            }

            if (GUILayout.Toggle(selectedTabIndex == 1, String_Settings, EditorStyles.toolbarButton))
            {
                selectedTabIndex = 1;
            }

            EditorGUILayout.EndHorizontal();
        }
        #endregion

        #region Tabs

        

        private void DrawMainTab()
        {
            EditorGUILayout.BeginHorizontal();
            DrawWaveSegment();
            GUILayout.Space(2);
            DrawPropertiesSegment();
            GUILayout.Space(2);
            DrawObjectsSegment();
            // GUILayout.Space(2);
            // DrawObjectsButtons();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettingsTab()
        {
            EditorGUILayout.BeginHorizontal();
            DrawSpawnerDataSegment();
            EditorGUILayout.EndHorizontal();
        }


        private void DrawSpawnerDataSegment()
        {
            DrawBackgroundVertical(widthDataListSegment, backgroundColumns);
            DrawSegmentTitleH1(String_SpawnerData);
            DrawBackgroundHorizontal(widthDataListSegment);
           
            scrollPositionSpawnDataSegment = EditorGUILayout.BeginScrollView(scrollPositionSpawnDataSegment, GUILayout.ExpandHeight(true));

            for (int i = 0; i < spawnerDataList.Count; i++)
            {
                if (GUILayout.Button(spawnerDataList[i].name, EditorStyles.toolbarButton))
                {
                    selectedSpawnerData = spawnerDataList[i];
                    selectedSpawnerDataIndex = i;
                    RefreshWaveList();
                }
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            
            if (GUILayout.Button(String_Refresh, GUILayout.Height(40)))
            {
                RefreshSpawnerDataList();
                GUILayout.Space(10);
            }

            EditorGUILayout.EndHorizontal();
        }
        #endregion

        #region Segments
        /// <summary>
        /// Draw Wave column (left)
        /// </summary>
        private void DrawWaveSegment()
        {
            DrawBackgroundVertical(widthWaveSegment, backgroundColumns);
            DrawSegmentTitleH1(String_Waves);
            DrawBackgroundHorizontal();

            scrollPositionWaveSegment = EditorGUILayout.BeginScrollView(scrollPositionWaveSegment, GUILayout.ExpandHeight(true));

            if (selectedSpawnerData)
            {
                // '1' is the number of columns
                selectedWaveIndex = GUILayout.SelectionGrid(selectedWaveIndex, waveOptions, 1, EditorStyles.miniButtonMid);
            }
            else
            {
                RefreshData();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button(String_Refresh, GUILayout.Height(20)))
            {
                RefreshWaveList();
            }
            
            if (GUILayout.Button(String_RemoveLastWave, GUILayout.Height(20)))
            {
                selectedSpawnerData.RemoveLastWave();
                RefreshWaveList();
                SaveSelectedSpawnerData();
            }

            if (GUILayout.Button(String_CreateWave, GUILayout.Height(40)))
            {
                selectedSpawnerData.CreateNewWave();
                RefreshWaveList();
                SaveSelectedSpawnerData();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw properties column (middle)
        /// </summary>
        private void DrawPropertiesSegment()
        {
            DrawBackgroundVertical(widthPropertiesSegment, backgroundSegment);
            DrawSegmentTitleH1(String_Properties);
            DrawBackgroundHorizontal(widthPropertiesSegment);

            scrollPositionPropertiesSegment = EditorGUILayout.BeginScrollView(scrollPositionPropertiesSegment, GUILayout.ExpandHeight(true));

            if (CheckIf_SelectedWave())
            {
                    // Save the old value of the wave name
                    string oldWaveName = selectedWave.waveName;

                    // Display a text field for editing the wave name
                    selectedWave.waveName = CustomTextFieldWithLabelOnLeft(String_WaveName, selectedWave.waveName);

                    // Check whether the new value differs from the old value
                    if (!string.Equals(oldWaveName, selectedWave.waveName))
                    {
                        RefreshWaveList();
                    }

                    // Base properites
                    GUILayout.Space(10);
                    selectedWave.waveEndCondition = (WaveEndCondition)EditorGUILayout.EnumPopup(String_WaveEndCondition, selectedWave.waveEndCondition);
                    selectedWave.autoStartNextWave = EditorGUILayout.Toggle(String_AutoStartNextWave, selectedWave.autoStartNextWave);
                    selectedWave.spawnStartDelay = EditorGUILayout.FloatField(String_SpawnStartDelayS, selectedWave.spawnStartDelay);
                    selectedWave.spawnEverySecond = EditorGUILayout.FloatField(String_SpawnEveryS, selectedWave.spawnEverySecond);
                    EditorGUILayout.Space();

                    
                    switch (selectedWave.waveEndCondition)
                    {
                        // case WaveEndCondition.None:
                        //     break;
                        case WaveEndCondition.Time:
                            selectedWave.spawnDurationMinutes = EditorGUILayout.FloatField("Spawn Duration (m)", selectedWave.spawnDurationMinutes);
                            selectedWave.spawnDurationSeconds = EditorGUILayout.FloatField("Spawn Duration (s)", selectedWave.spawnDurationSeconds);
                            break;
                        case WaveEndCondition.SpawnedCount:
                            selectedWave.requiredSpawnCount = EditorGUILayout.IntField("Required Spawns", selectedWave.requiredSpawnCount);
                            break;
                        case WaveEndCondition.ExternalEvent:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();

            if (CheckIf_CanSelectPrevious())
            {
                if (GUILayout.Button("Copy properties from previous", GUILayout.Height(20)))
                {
                    selectedWave.CopyPropertiesFrom(previousSelectedWave);
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawObjectsSegment()
        {
            DrawBackgroundVertical(widthObjectsSegment, backgroundSegment);
            DrawSegmentTitleH1(String_Objects);
            DrawBackgroundHorizontal(widthObjectsSegment);

            scrollPositionObjectsSegment = EditorGUILayout.BeginScrollView(scrollPositionObjectsSegment, GUILayout.ExpandHeight(true));

            if (CheckIf_SelectedWave() && CheckIf_DrawObjectsSegment())
            {
                for (int i = 0; i < selectedWave.objectsToSpawn.Count; i++)
                {
                    var enemyInfo = selectedWave.objectsToSpawn[i];

                    // Draw background for each item
                    DrawBackgroundVertical(0, backgroundObjectsListItem);
                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    StartObserveChanges();
                    EditorGUILayout.LabelField(String_Prefab, GUILayout.Width(100)); // Reduced label width
                    enemyInfo.prefab = (GameObject)EditorGUILayout.ObjectField(enemyInfo.prefab, typeof(GameObject), false);
                    StopObserveChanges();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    StartObserveChanges();
                    EditorGUILayout.LabelField(String_SpawnChange, GUILayout.Width(100)); // Reduced label width
                    enemyInfo.spawnChance = EditorGUILayout.IntSlider(enemyInfo.spawnChance, 0, 100);
                    EditorGUILayout.EndHorizontal();
                    StopObserveChanges();

                    // Add Remove button
                    if (GUILayout.Button(String_Remove, GUILayout.Width(60)))
                    {
                        selectedWave.objectsToSpawn.RemoveAt(i);
                        SaveSelectedSpawnerData();
                        EditorGUILayout.EndVertical();
                        break; // Exit the loop to avoid modifying the list while iterating
                    }
                    
                    GUILayout.Space(5);
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(15);
                }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
            
            if (CheckIf_SelectedWave())
            {
                // Adding a button to add new objects to the spawn
                if (GUILayout.Button(String_AddNewObjectToSpawn, GUILayout.Height(20)))
                {
                    selectedWave.AddObjectToSpawn(null);
                    SaveSelectedSpawnerData();
                }

                // Adding a button to remove the last object from the list
                if (GUILayout.Button(String_RemoveLastObject, GUILayout.Height(20)))
                {
                    if (selectedWave.objectsToSpawn.Count > 0)
                    {
                        selectedWave.objectsToSpawn.RemoveAt(selectedWave.objectsToSpawn.Count - 1);
                        SaveSelectedSpawnerData();
                    }
                }

                if (selectedWaveIndex > 0)
                {
                    // Adding a button to remove the last object from the list
                    if (GUILayout.Button(String_CopyListFromPreviousWave, GUILayout.Height(20)))
                    {
                        selectedWave.objectsToSpawn =
                            new List<ObjectSpawnInfo>(selectedSpawnerData.waveDataList[selectedWaveIndex - 1].objectsToSpawn);
                        SaveSelectedSpawnerData();
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
        }

        private void DrawObjectsButtons()
        {
            DrawBackgroundVertical(widthObjectsHelperSegment, backgroundSegment);
            DrawSegmentTitleH1(String_ObjectsHelper);

            if (CheckIf_SelectedWave())
            {
                // Adding a button to add new objects to the spawn
                if (GUILayout.Button(String_AddNewObjectToSpawn, GUILayout.Height(40)))
                {
                    selectedWave.AddObjectToSpawn(null);
                    SaveSelectedSpawnerData();
                }

                // Adding a button to remove the last object from the list
                if (GUILayout.Button(String_RemoveLastObject, GUILayout.Height(40)))
                {
                    if (selectedWave.objectsToSpawn.Count > 0)
                    {
                        selectedWave.RemoveLast();
                        SaveSelectedSpawnerData();
                    }
                }

                // Adding a button to remove the last object from the list
                if (GUILayout.Button(String_ClearList, GUILayout.Height(20)))
                {
                    selectedWave.objectsToSpawn.Clear();
                    SaveSelectedSpawnerData();
                }
                
                if (selectedWaveIndex > 0)
                {
                    // Adding a button to remove the last object from the list
                    if (GUILayout.Button(String_CopyListFromPreviousWave, GUILayout.Height(20)))
                    {
                        selectedWave.objectsToSpawn =
                            new List<ObjectSpawnInfo>(selectedSpawnerData.waveDataList[selectedWaveIndex - 1].objectsToSpawn);
                        
                        SaveSelectedSpawnerData();
                    }
                }
            }
            
            //EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        
        #endregion

        #region SavingChanges
        private void SaveSelectedSpawnerData()
        {
            EditorUtility.SetDirty(selectedSpawnerData);
            AssetDatabase.SaveAssets();        
        }

        private void StartObserveChanges()
        {
            EditorGUI.BeginChangeCheck();
        }

        // If there was a change, mark the data as dirty
        private void StopObserveChanges()
        {
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(selectedSpawnerData);
                AssetDatabase.SaveAssets();
            }
        }

        #endregion

        #region Checks
        private bool CheckIf_SelectedWave()
        {
            if (selectedSpawnerData != null )
            {
                //for be sure to always have selected wave
                RefreshSelection();

                if (selectedWave != null)
                {
                    return true;
                }
                else
                {
                    selectedWave = null;
                    return false;
                }
            }
            
            return false;
        }

        private bool CheckIf_DrawObjectsSegment()
        {
            if (selectedWave.objectsToSpawn == null)
                return false;
            if (selectedWave.objectsToSpawn.Count == 0)
                return false;
            
            return true;
        }

        private bool CheckIf_CanSelectPrevious()
        {
            return selectedWaveIndex > 0;
        }
        
        #endregion

        #region Refresh

        private void RefreshSelection()
        {
            if (selectedSpawnerData != null)
            {
                if (selectedSpawnerData.waveDataList == null)
                    return;

                if (selectedSpawnerData.waveDataList.Count == 0)
                    return;
                
                if (selectedWaveIndex > selectedSpawnerData.waveDataList.Count-1)
                    selectedWaveIndex = selectedSpawnerData.waveDataList.Count-1;

                if (selectedWaveIndex < 0)
                    selectedWaveIndex = 0;
              
         
                //for be sure to always have selected wave
                selectedWave = selectedSpawnerData.waveDataList[selectedWaveIndex];

                if (CheckIf_CanSelectPrevious())
                    previousSelectedWave = selectedSpawnerData.waveDataList[selectedWaveIndex-1];
            }
        }

        private void RefreshData()
        {
            if (Selection.activeGameObject != null)
            {
                SpawnManager spawnerManager = Selection.activeGameObject.GetComponent<SpawnManager>();
                if (spawnerManager != null)
                {
                    if (spawnerManager.spawnerData)
                    {
                        selectedSpawnerData = spawnerManager.spawnerData;
                        selectedSpawnerDataIndex = spawnerDataList.IndexOf(selectedSpawnerData);
                    }
                    else
                    {
                        if (spawnerDataList.Count > 0)
                            selectedSpawnerData = spawnerDataList[0];
                        else
                            selectedSpawnerData = null;
                    }
                }
                else
                {
                    if (spawnerDataList.Count > 0)
                        selectedSpawnerData = spawnerDataList[0];
                    else
                        selectedSpawnerData = null;
                }
            }
            else
            {
                if (spawnerDataList.Count > 0)
                    selectedSpawnerData = spawnerDataList[0];
                else
                    selectedSpawnerData = null;
            }

            Repaint();
        }

        private void RefreshWaveList()
        {
            if (selectedSpawnerData == null)
            {
                Debug.LogError("selectedSpawnerData = null");
                return;
            }


            if (selectedSpawnerData.waveDataList == null)
            {
                Debug.LogError("selectedSpawnerData.waveDataList");
                return;
            }
            
            // Create an array of strings representing waves
            waveOptions = selectedSpawnerData.waveDataList
                .Select(wave => wave.waveName)
                .ToArray();
        }
        
        private void RefreshSpawnerDataList()
        {
            spawnerDataList = AssetDatabase.FindAssets(String_TSpawnerdata)
                .Select(guid => AssetDatabase.LoadAssetAtPath<SpawnerData>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();
        }
        
        #endregion

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
            if (width == 0 && _backgroundColor1 == default)
            {
                EditorGUILayout.BeginHorizontal();
            }
            else if (width == 0)
            {
                EditorGUILayout.BeginHorizontal(BackgroundStyle.Get(_backgroundColor1));
            }
            else
            {
                EditorGUILayout.BeginHorizontal(BackgroundStyle.Get(_backgroundColor1), (GUILayout.Width(width)));
            }
        }

        private void DrawSegmentTitleH1(string label)
        {
            if (styleH1 == null)
            {
                styleH1 = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Bold,
                    normal = { textColor = greyLight },
                    alignment = TextAnchor.MiddleCenter
                };
            }

            GUILayout.Space(10f);

            // Draw rect background
            Rect rect = EditorGUILayout.GetControlRect(false, 25);
            EditorGUI.DrawRect(rect, backgroundTitle1);

            GUI.Label(rect, label, styleH1);
            GUILayout.Space(10f);
        }

        private void CreateLabelStyle()
        {
            if (largeLabelStyle == null)
            {
                largeLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 20,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleLeft
                };
            }
        }

        #region CusomFields

        string CustomTextFieldWithLabelOnRight(string label, string value)
        {
            EditorGUILayout.BeginHorizontal();
            value = EditorGUILayout.TextField(value, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField(label, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
            return value;
        }

        string CustomTextFieldWithLabelOnLeft(string label, string value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(100));
            value = EditorGUILayout.TextField(value, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
            return value;
        }

        float CustomTextFieldWithLabelOnLeft(string label, float value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(100));
            value = EditorGUILayout.FloatField(value, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
            return value;
        }

        #endregion

    }

    public static class BackgroundStyle
    {
        private static Dictionary<Color, GUIStyle> backgroundStyles = new Dictionary<Color, GUIStyle>();

        public static GUIStyle Get(Color color)
        {
            if (!backgroundStyles.TryGetValue(color, out GUIStyle style))
            {
                style = new GUIStyle();
                style.normal.background = MakeTex(1, 1, color);
                backgroundStyles[color] = style;
            }

            return style;
        }

        public static void ResetBackgroundStyle()
        {
            backgroundStyles.Clear();
        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}