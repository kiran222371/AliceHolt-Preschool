using UnityEngine;

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "LocationEvent", menuName = "Location System/Location Event")]
    public class LocationEvent : ScriptableObject
    {
        public GPSCoord location;
        [Tooltip("The radius in meters in which the action will be triggered")]
        [Range(5, 100)]
        public float radiusM;
        public LocationActionBase[] onLocationReached;
    }

}