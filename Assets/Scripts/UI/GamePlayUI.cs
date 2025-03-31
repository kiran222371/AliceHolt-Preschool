using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    [Header("Dialouge Animation")]
    [HideInInspector] public VisualElement dialougeBox;
    [SerializeField] private float charPerSec;
    private VisualElement root;
    private Label speakerLabel;
    private Label dialougeLabel;
    private Coroutine dialougeRoutine;
    private StoryScriptPlayer scriptPlayer;
    private bool choiceShown;
    void Start()
    {
        root = FindFirstObjectByType<UIDocument>().rootVisualElement;
        speakerLabel = root.Q<Label>("SpeakerLabel");
        dialougeLabel = root.Q<Label>("DialougeLabel");
        dialougeBox = root.Q<VisualElement>("DialougeBox");
        scriptPlayer = FindFirstObjectByType<StoryScriptPlayer>();
        dialougeBox.AddManipulator(new Clickable(() => { scriptPlayer?.NextLine(null); }));
        scriptPlayer?.NextLine(null);

        root.Q<Button>("HomeButton").clicked += () =>
        {
            SceneManager.LoadScene("Menu");
        };
    }

    void OnValidate()
    {
        if (charPerSec < 0)
        {
            charPerSec = 1;
            Debug.LogWarning("CharPerSec not valid, defaulting to 1");
        }
    }

    public void ShowChoices()
    {
        if(choiceShown)
            return;
        var choicoePanel = root.Q<VisualElement>("ChoicePanel");
        choicoePanel.style.opacity = 1;
        var choice = root.Q<Button>("Choice");
        choice.text = "Return to Menu";
        choice.clicked += () =>
        {
            SceneManager.LoadSceneAsync("Menu");
        };
        choiceShown = true;
    }

    public void ShowDialouge(StoryScript.Line line)
    {
        if (dialougeRoutine != null)
            StopCoroutine(dialougeRoutine);
        dialougeRoutine = StartCoroutine(TextAnim(line.speaker, line.text));
    }


    private IEnumerator TextAnim(string speaker, string text)
    {
        speakerLabel.text = speaker;
        dialougeLabel.text = "";
        int charIndex = 0;
        while (charIndex < text.Length)
        {
            dialougeLabel.text += text[charIndex++];
            yield return new WaitForSeconds(1 / charPerSec);
        }
    }
}