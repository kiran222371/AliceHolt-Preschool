using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

[CreateAssetMenu(fileName = "StoryName", menuName = "Story/StoryScript")]
public class StoryScript : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string title;
        public string description;
        [Tooltip("A unique index that identifies the story")]
        public int index;
        public List<string> characters;
    }
    [System.Serializable]
    public struct Line
    {
        public string speaker;
        public string text;
        public AudioClip voice;
    }
    public Info info;
    public Line[] lines;

    void OnValidate()
    {
        info.characters.Clear();
        foreach (Line line in lines)
        {
            if (info.characters.Contains(line.speaker) == false)
            {
                info.characters.Add(line.speaker);
            }
        }
    }
}

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(StoryScript.Line))]
    public class LinePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var horizontalContainer = new VisualElement();
            horizontalContainer.style.flexDirection = FlexDirection.Row;

            var leftContainer = new VisualElement();
            leftContainer.style.flexBasis = 10;
            leftContainer.style.flexGrow = 1;
            leftContainer.style.marginRight = 10;
            horizontalContainer.Add(leftContainer);

            var rightContainer = new VisualElement();
            rightContainer.style.flexBasis = 10;
            rightContainer.style.flexGrow = 3;
            horizontalContainer.Add(rightContainer);

            var speakerProperty = property.FindPropertyRelative("speaker");
            var textProperty = property.FindPropertyRelative("text");
            var voiceProperty = property.FindPropertyRelative("voice");

            var speakerField = new PropertyField(speakerProperty, "");
            var voiceField = new PropertyField(voiceProperty, "");
            var textField = new TextField();
            textField.bindingPath = textProperty.propertyPath;
            textField.multiline = true;
            textField.style.whiteSpace = WhiteSpace.Normal;


            leftContainer.Add(speakerField);
            leftContainer.Add(voiceField);
            rightContainer.Add(textField);

            return horizontalContainer;
        }
    }
#endif