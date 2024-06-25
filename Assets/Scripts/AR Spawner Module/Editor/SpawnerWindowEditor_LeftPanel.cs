using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AR_Spawner_Module.Editor
{
    public partial class SpawnerWindowEditor : EditorWindow
    {
        // private string[] waveOptions;
        // public void DrawLeftSegment()
        // {
        //     DrawBackgroundVertical(200, backgroundColorDark);
        //     DrawSectionTitleH1(String_Waves);
        //     DrawBackgroundHorizontal(200, backgroundColor1);
        //
        //      scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
        //     
        //     if (selectedSpawnerData)
        //     {
        //         RefreshWaveList();
        //         selectedWaveIndex = GUILayout.SelectionGrid(selectedWaveIndex, waveOptions, 1, EditorStyles.miniButtonMid); // '1' to liczba kolumn
        //     }
        //     else
        //     {
        //         RefreshData();
        //     }
        //     
        //     EditorGUILayout.EndVertical();
        //     EditorGUILayout.EndScrollView();
        //     if (GUILayout.Button("Create Wave", GUILayout.Height(40)))
        //     {
        //     }
        //     EditorGUILayout.EndHorizontal();
        //
        //    
        //
        // }

        // private void RefreshWaveList()
        // {
        //     if (waveOptions == null || waveOptions.Length == 0)
        //     {
        //         // Tworzenie tablicy stringów reprezentujących fale
        //         waveOptions = selectedSpawnerData.waveDataList
        //             .Select(wave => wave.waveName) // Załóżmy, że każda fala ma pole 'waveName'
        //             .ToArray();
        //     }
        // }
    }
}