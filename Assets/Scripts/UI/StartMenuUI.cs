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
            root.Q<VisualElement>("SettingsPanel"),
            root.Q<VisualElement>("AboutPanel")
        };
        root.Q<Button>("Start").clicked += () => { SceneManager.LoadScene("1"); };
        root.Q<Button>("Settings").clicked += () =>PanelToggle(1);
        root.Q<Button>("About").clicked += () => PanelToggle(2);
        root.Q<Button>("CloseSettings").clicked += () => PanelToggle(0);
        root.Q<Button>("CloseAbout").clicked += () => PanelToggle(0);
        root.Q<Button>("Exit").clicked += () => { Application.Quit(); };
    }

    void PanelToggle(int panelIndex)
    {
        Debug.Log("PanelToggle: " + panelIndex);
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].style.flexGrow = i == panelIndex ? 1 : 0;
        }

    }

}