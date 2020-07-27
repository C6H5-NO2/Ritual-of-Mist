using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "DefaultEvDatDict", menuName = "Adventure/Event Data Dictionary")]
    public class EvDatDict : ScriptableObject {
        public static EvDatDict Instance { get; private set; }

        public IdSoDict<EventData> Dict { get; private set; }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if(Instance != null)
                return;
            Instance = Resources.Load<EvDatDict>("SOs/Dicts/DefaultEvDatDict");
            var sos = Resources.LoadAll<EventData>("SOs/EventData");
            Instance.Dict = new IdSoDict<EventData>(sos, true);

            foreach(var so in sos)
                // todo: read from json
                so.TriggeredTimes = 0;
        }
    }
}
