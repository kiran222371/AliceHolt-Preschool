using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoryScriptPlayer : MonoBehaviour
{
    public StoryScript script;
    public int lineIndex = -1;
    private AudioSource audioSource;
    //[SerializeField] private SMap<string, XRBaseInteractable> interactables;
    //private string activeCharacter = "";
    private GamePlayUI gamePlayUI;
    private void Awake()
    {
        if (script == null)
        {
            Debug.LogError("No script assigned to StoryScriptPlayer");
            return;
        }
        // foreach (string character in script.info.characters)
        // {
        //     if (!interactables.ContainsKey(character))
        //     {
        //         Debug.LogError($"No interactable assigned to {character}");
        //     }
        // }
        // activeCharacter = script.lines[0].speaker;


        audioSource = GetComponent<AudioSource>();
        
        gamePlayUI = FindFirstObjectByType<GamePlayUI>();


    }
    public void NextLine(SelectEnterEventArgs args)
    {
        lineIndex++;

        StoryScript.Line line = script.lines[lineIndex];
        gamePlayUI?.ShowDialouge(line);

        if (line.voice != null)
        {
            audioSource.clip = line.voice;
            audioSource.Play();
        }
    }

    // void OnValidate()
    // {
    //     if(script == null)
    //     {
    //         return;
    //     }

    //     var newInteractables = new SMap<string, XRBaseInteractable>();
    //     foreach (string character in script.info.characters)
    //     {
    //         if (interactables.ContainsKey(character))
    //         {
    //             newInteractables[character] = interactables[character];
    //         }
    //         else
    //         {
    //             newInteractables[character] = null;
    //         }
    //     }
    //     interactables = newInteractables;
    // }
}