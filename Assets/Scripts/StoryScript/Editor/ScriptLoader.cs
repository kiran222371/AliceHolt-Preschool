using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ScriptLoader : EditorWindow
{
    [SerializeField] private StoryScript storyScript;
    private SerializedObject serializedObject;
    private string scriptText;

    [MenuItem("Preschool/Script Loader")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor.
        EditorWindow wnd = GetWindow<ScriptLoader>(true);
        //wnd.titleContent = new GUIContent("Script Loader");

        // Limit size of the window.

    }

    public void OnGUI()
    {
        storyScript = (StoryScript)EditorGUILayout.ObjectField("Load to", storyScript, typeof(StoryScript), false);
        if (GUILayout.Button("Load Script"))
        {
            var matches = new Regex(@"(\w+).*:\s*(.+)\s*").Matches(scriptText);
            storyScript.lines = new StoryScript.Line[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                storyScript.lines[i] = new StoryScript.Line
                {
                    speaker = match.Groups[1].Value,
                    text = match.Groups[2].Value
                };
            }
        }

        //scriptText = EditorGUILayout.TextArea(scriptText, GUILayout.);
        scriptText = GUILayout.TextArea(scriptText);


    }

}