using UnityEditor;
using UnityEngine;

namespace AR_Spawner_Module.Editor
{
    // public enum SpawnType
    // {
    //     ByTime,
    //     ByCount,
    //     ByTotalCount,
    //     
    // }
    // public partial class SpawnerWindowEditor : EditorWindow
    // {
    //     private string AbilityName;
    //     private string myValue;
    //     private SpawnType spawnType;
    //     private float time;
    //     
    //     public void DrawMiddleSegment()
    //     {
    //         DrawBackgroundVertical(250, backgroundColorDark);
    //         DrawSectionTitleH1(String_Properties);
    //         DrawBackgroundHorizontal(250, backgroundColor1);
    //
    //         scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
    //        
    //         WaveData selectedWave = selectedSpawnerData.waveDataList[selectedWaveIndex];
    //
    //         if (selectedWave != null)
    //         {
    //             myValue = CustomTextFieldWithLabelOnLeft("Wave name", myValue);
    //             spawnType = CustomEnumFieldWithLabelOnLeft("Type", spawnType);
    //            
    //             GUILayout.Space(10);
    //             time = CustomTextFieldWithLabelOnLeft("Spawn every", time);
    //             
    //         }
    //
    //         EditorGUILayout.EndScrollView();
    //         EditorGUILayout.EndHorizontal();
    //         EditorGUILayout.EndVertical();
    //     }
    //     
    //     string CustomTextFieldWithLabelOnRight(string label, string value)
    //     {
    //         EditorGUILayout.BeginHorizontal();
    //         value = EditorGUILayout.TextField(value, GUILayout.ExpandWidth(true));
    //         
    //         EditorGUILayout.LabelField(label, GUILayout.Width(100)); // Ustaw szerokość etykiety
    //         EditorGUILayout.EndHorizontal();
    //         return value;
    //     }
    //     
    //     string CustomTextFieldWithLabelOnLeft(string label, string value)
    //     {
    //         EditorGUILayout.BeginHorizontal();
    //         
    //         EditorGUILayout.LabelField(label, GUILayout.Width(100)); // Ustaw szerokość etykiety
    //         value = EditorGUILayout.TextField(value, GUILayout.ExpandWidth(true));
    //
    //         EditorGUILayout.EndHorizontal();
    //         return value;
    //     }
    //     
    //     float CustomTextFieldWithLabelOnLeft(string label, float value)
    //     {
    //         EditorGUILayout.BeginHorizontal();
    //         
    //         EditorGUILayout.LabelField(label, GUILayout.Width(100)); // Ustaw szerokość etykiety
    //         value = EditorGUILayout.FloatField(value, GUILayout.ExpandWidth(true));
    //
    //         EditorGUILayout.EndHorizontal();
    //         return value;
    //     }
    //     
    //     SpawnType CustomEnumFieldWithLabelOnLeft(string label, SpawnType value)
    //     {
    //         EditorGUILayout.BeginHorizontal();
    //         
    //         EditorGUILayout.LabelField(label, GUILayout.Width(100)); // Ustaw szerokość etykiety
    //         value =  (SpawnType)EditorGUILayout.EnumPopup(value, GUILayout.ExpandWidth(true));
    //         EditorGUILayout.EndHorizontal();
    //         return value;
    //     }
    // }
}