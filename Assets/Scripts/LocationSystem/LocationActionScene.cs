using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "LoadScene", menuName = "Location System/Action/Scene")]
    public class LocationActionScene : LocationActionBase
    {
        [Tooltip("The name of the scene to load")]
        public string nextScene;

        public override void Invoke()
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
    }
}