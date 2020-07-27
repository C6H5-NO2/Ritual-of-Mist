using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "Location", menuName = "Adventure/Location Data")]
    public class LocationData : IdScriptableObject {
        public Vector2 locSymbolPos;
        public int goldCost;
        public float timeCost;
        public Sprite[] uibgs = new Sprite[3];
        public EventData[] events;


        // todo: consider using co-routine / multi-thread
        public IEventTrigger trigger; // set in LocDatDict

        // todo: write to json
        public bool Available { get; set; } // set in LocDatDict

        // todo: write to json
        /// <summary> increases BEFORE event triggered </summary>
        public int VisitTimes { get; set; }

        public void OnVisit() => ++VisitTimes;
    }


    public interface IEventTrigger {
        /// <param name="events"> should be events in this location </param>
        /// <param name="time"> time when called </param>
        /// <param name="weather"> weather when called </param>
        /// <param name="hand"></param>
        /// <param name="loots"></param>
        /// <returns> bool mask of whether event succeeds </returns>
        bool[] Trigger(EventData[] events, Zeit time, Weather weather, (uint, uint, uint) hand, out List<uint> loots);
    }
}
