using UnityEngine;

namespace Prechool.LocationSystem
{
    [CreateAssetMenu(fileName = "Map Meta Data", menuName = "Location System/Map Meta Data")]
    public class MapMeta : ScriptableObject
    {
        public Texture locSymbol;
        public Texture forestMap;
        public GPSCoord topLeftCoord = new GPSCoord(51.18345f, -0.8520539f);
        public GPSCoord bottomRightCoord = new GPSCoord(51.15710f, -0.8260767f);
        [Header("↓ Read Only ↓")]
        [SerializeField] private float mapWidthMetres;
        public float MapWidthMetres => mapWidthMetres;
        [SerializeField] private float mapHeightMetres;
        public float MapHeightMetres => mapHeightMetres;
        private const float meterPerLatDeg = 111111;

        void OnEnable()
        {
            InitVals();
        }

        void OnValidate()
        {
            InitVals();
        }

        private void InitVals()
        {
            GPSCoord diffCoord = new GPSCoord(topLeftCoord.latiude - bottomRightCoord.latiude,
                bottomRightCoord.longitude - topLeftCoord.longitude);
            mapHeightMetres = diffCoord.latiude * meterPerLatDeg;
            mapWidthMetres = diffCoord.longitude *
             Mathf.Cos(topLeftCoord.latiude * Mathf.PI / 180) * meterPerLatDeg;
        }
    }

}