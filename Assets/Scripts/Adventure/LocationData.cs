using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "Location", menuName = "Location/Location Data")]
    public class LocationData : Utils.IdScriptableObject {
        public Vector2 locSymbolPos;
        public int goldCost;
        public float timeCost;
        public LocationEvent[] events;

        // todo: check events here
    }
}
