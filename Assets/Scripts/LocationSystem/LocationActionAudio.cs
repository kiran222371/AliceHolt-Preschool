using UnityEngine;
#if UnityEditor
using UnityEditor;
#endif

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "PlayAudio", menuName = "Location System/Action/Audio")]
    public class LocationActionAudio : LocationActionBase
    {
        public AudioClip audio;

        public override void Invoke()
        {
            AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
        }
    }
}