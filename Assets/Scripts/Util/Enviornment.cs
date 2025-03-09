using Prechool.Character;
using UnityEngine;

public class Enviornment : MonoBehaviour
{
    public Material foilageMat;
    private Character[] characters;
    private void Start()
    {
        characters = FindObjectsByType<Character>(FindObjectsSortMode.None);
    }
    void Update()
    {
        foilageMat.SetVector("_CharPos", characters[0].transform.position);
    }
}