
using NUnit.Framework;
using Prechool.LocationSystem;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

public class LocationSystemTest
{
    [Test]
    public void LocationSystemParameterTest()
    {
        string[] buildScenePaths = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < buildScenePaths.Length; i++)
        {
            buildScenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        var buildSceneNames = buildScenePaths.Select(x => System.IO.Path.GetFileNameWithoutExtension(x)).ToArray();

        var holders = MyEditorUtils.FindAssets<LocationEventHolder>();
        var holder = holders[0];

        Assert.AreEqual(holders.Length, 1);

        foreach (var locationEvent in holder.locationEvents)
        {
            var locationEventPath = AssetDatabase.GetAssetPath(locationEvent);
            Assert.NotZero(locationEvent.location.latiude, locationEventPath);
            Assert.NotZero(locationEvent.location.longitude, locationEventPath);

            foreach (var action in locationEvent.onLocationReached)
            {
                var actionPath = AssetDatabase.GetAssetPath(action);
                switch (action)
                {
                    case LocationActionAudio audio:
                        Assert.IsNotNull(audio.audio, actionPath);
                        break;
                    case LocationActionScene scene:
                        Assert.Contains(scene.nextScene, buildSceneNames, actionPath);
                        break;
                    default:
                        Assert.Fail("Unknown action type");
                        break;
                }
            }
        }

    }
}
