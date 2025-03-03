using UnityEngine;

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "LocationEventHolder", menuName = "Location System/Location Event Holder")]
    public class LocationEventHolder : ScriptableObject
    {
       
        public LocationEvent[] locationEvents;
    }
}
