using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.NewAdv {
    [CreateAssetMenu(fileName = "DefaultLocDatDict", menuName = "Adventure/Location Data Dictionary")]
    public class LocDatDict : ScriptableObject {
        public static LocDatDict Instance { get; private set; }

        public IdSoDict<LocationData> Dict { get; private set; }

        // todo
        //[RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if(Instance != null)
                return;
            Instance = Resources.Load<LocDatDict>("SOs/DefaultLocDatDict");
            var sos = Resources.LoadAll<LocationData>("SOs/LocationData");
            Instance.Dict = new IdSoDict<LocationData>(sos.Length);
            foreach(var so in sos) {
                switch(so.id) {
                    // todo
                    default:
                        so.trigger = null;
                        break;
                }
                Instance.Dict.Add(so, true);
            }
        }
    }
}
