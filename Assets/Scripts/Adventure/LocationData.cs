using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.NewAdv {
    public interface IEventTrigger {
        /// <param name="events"> should be events in this location </param>
        /// <param name="time"> time when called </param>
        /// <param name="weather"> weather when called </param>
        /// <param name="hand"></param>
        /// <param name="loots"></param>
        /// <returns> bool mask of whether event succeeds </returns>
        bool[] Trigger(EventData[] events, Zeit time, Weather weather, (uint, uint, uint) hand, out List<uint> loots);
    }


    [CreateAssetMenu(fileName = "Location", menuName = "Adventure/Location Data")]
    public class LocationData : IdScriptableObject {
        public Vector2 locSymbolPos;
        public int goldCost;
        public float timeCost;
        public Sprite[] uibgs = new Sprite[3];
        public EventData[] events;

        // set in LocDatDict
        // todo: consider using co-routine / multi-thread
        public IEventTrigger trigger;

        // todo: write to file
        public bool Available { get; set; }
        public int VisitTimes { get; private set; }

        public void OnVisit() => ++VisitTimes;
    }
}
