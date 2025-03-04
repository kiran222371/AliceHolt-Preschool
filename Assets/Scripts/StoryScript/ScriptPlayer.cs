using UnityEngine;
using UnityEngine.InputSystem;

public class StoryScriptPlayer : MonoBehaviour
{
    public StoryScript script;
    public int lineIndex = -1;
    public InputActionReference nextLineAction;
    private AudioSource audioSource;
    public void OnEnable()
    {
        nextLineAction.action.Enable();
        nextLineAction.action.performed += ToNextLine;
    }
    public void OnDisable()
    {
        nextLineAction.action.performed -= ToNextLine;
    }
    public void NextLine()
    {
        lineIndex++;
        StoryScript.Line line = script.lines[lineIndex];
        string msg = $"{line.speaker}: {line.text}";
        Debug.Log(msg);
        if(line.voice != null)
        {
            audioSource.clip = line.voice;
            audioSource.Play();
        }
    }
    private void ToNextLine(InputAction.CallbackContext context)
    {
        NextLine();
    }
}