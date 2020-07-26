using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.NewAdv {
    [CreateAssetMenu(fileName = "Event", menuName = "Adventure/Event Data")]
    public class EventData : IdScriptableObject {
        // todo: write to file
        public int TriggeredTimes { get; private set; }

        public void OnSucceed() => ++TriggeredTimes;
    }
}
