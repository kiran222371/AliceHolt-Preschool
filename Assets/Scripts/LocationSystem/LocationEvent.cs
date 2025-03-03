using UnityEngine;

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "LocationEvent", menuName = "Location System/Location Event")]
    public class LocationEvent : ScriptableObject
    {
        public GPSCoord location;
        public LocationActionBase[] onLocationReached;
    }

}