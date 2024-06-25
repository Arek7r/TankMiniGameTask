using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class WelcomeWindow : EditorWindow
{
    private GUIStyle styleH1;
    private GUIStyle styleH2;
    private Color greyLight = new Color(0.7f, 0.7f, 0.7f);
    private Color white = new Color(0.5f, 0.5f, 0.5f);
    static string showedTodoWindowSessionStateName = "TodoWindowEditor.showedTodoWindow";

    static WelcomeWindow()
    {
        EditorApplication.delayCall += ShowTodoWindowAutomatically;
    }

    static void ShowTodoWindowAutomatically()
    {
        if (!SessionState.GetBool(showedTodoWindowSessionStateName, false))
        {
            OpenWindow();
            SessionState.SetBool(showedTodoWindowSessionStateName, true);
        }
    }
  

    [MenuItem("Tools/Welcome window")]
    public static void OpenWindow()
    {
        GetWindow<WelcomeWindow>("Welcome window");
    }
    
    
    private void OnGUI()
    {
        DrawSectionTitleH1("TO DO LIST:");

        DrawSectionTitleH2("- Score System");
        DrawSectionTitleH2("- UI manager");
        DrawSectionTitleH2("- Enemy detector zone");
        GUILayout.Space(5);

        DrawSectionTitleH2("- Ability HUD");
        DrawSectionTitleH2("- AbilityType: Buff");

        GUILayout.Space(5);

        DrawSectionTitleH2("- Coroutine manager");
        DrawSectionTitleH2("- Ability: PressHold");
        DrawSectionTitleH2("- JOB for AI move");
        DrawSectionTitleH2("- Addressable loading manager");
        DrawSectionTitleH2("- Addressable pool loading(?)");
    }
    
    private void DrawSectionTitleH1(string label)
    {
        if (styleH1 == null)
        {
            styleH1 = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 20, 
                fontStyle = FontStyle.Bold,
                normal = {textColor = greyLight},
                alignment = TextAnchor.MiddleCenter
            };
        }
        
        GUILayout.Space(10f);
        GUILayout.Label(label, styleH1);
        GUILayout.Space(10f);
    }
    
    private void DrawSectionTitleH2(string label)
    {
        if (styleH2 == null)
        {
            styleH2 = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 12, 
                fontStyle = FontStyle.Normal,
                normal = {textColor = greyLight},
                alignment = TextAnchor.MiddleLeft
            };
        }
        
        GUILayout.Space(5f);
        GUILayout.Label(label, styleH2);
    }
}