using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "Event", menuName = "Adventure/Event Data")]
    public class EventData : IdScriptableObject {
        // todo: write to json
        public int TriggeredTimes { get; set; }

        public void OnTrigger() => ++TriggeredTimes;
    }
}
