using UnityEngine;

namespace Prechool.LocationSystem
{
    [System.Serializable]
    public class GPSCoord
    {
        [Tooltip("North-South coordinate")]
        public float latiude;
        [Tooltip("East-West coordinate")]
        public float longitude;

        public GPSCoord(float lat, float lon)
        {
            latiude = lat;
            longitude = lon;
        }
    }

    public abstract class LocationActionBase : ScriptableObject
    {
        public abstract void Invoke();
    }


}