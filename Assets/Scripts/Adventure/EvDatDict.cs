using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.NewAdv {
    [CreateAssetMenu(fileName = "DefaultEvDatDict", menuName = "Adventure/Event Data Dictionary")]
    public class EvDatDict : ScriptableObject {
        public static EvDatDict Instance { get; private set; }

        public IdSoDict<EventData> Dict { get; private set; }

        // todo
        //[RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if(Instance != null)
                return;
            Instance = Resources.Load<EvDatDict>("SOs/DefaultEvDatDict");
            var sos = Resources.LoadAll<EventData>("SOs/EventData");
            Instance.Dict = new IdSoDict<EventData>(sos, true);
        }
    }
}
