using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuUI : MonoBehaviour
{
    private VisualElement[] panels;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        panels = new VisualElement[] {
            root.Q<VisualElement>("StartPanel"),
            root.Q<VisualElement>("LevelsPanel"),
            root.Q<VisualElement>("AboutPanel")
        };
        root.Q<Button>("Start").clicked += () => StartGame("1");
        root.Q<Button>("Levels").clicked += () => PanelToggle(1);
        root.Q<Button>("About").clicked += () => PanelToggle(2);
        root.Q<Button>("CloseLevels").clicked += () => PanelToggle(0);
        root.Q<Button>("CloseAbout").clicked += () => PanelToggle(0);
        root.Q<Button>("Exit").clicked += () => { Application.Quit(); };

        for (int i = 0; i < 3; i++)
        {
            string sceneName = (i + 1).ToString();
            root.Q<Button>($"l{i + 1}").clicked += () => StartGame(sceneName);
        }
    }

    void PanelToggle(int panelIndex)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].style.flexGrow = i == panelIndex ? 1 : 0;
        }

    }

    void StartGame(string sceneName)
    {
        Debug.Log($"Starting game with scene: {sceneName}");
        SceneManager.LoadScene("Game Base");
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

}