using ThisGame.Adventure.EvTr;
using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "DefaultLocDatDict", menuName = "Adventure/Location Data Dictionary")]
    public class LocDatDict : ScriptableObject {
        public static LocDatDict Instance { get; private set; }

        public IdSoDict<LocationData> Dict { get; private set; }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if(Instance != null)
                return;
            Instance = Resources.Load<LocDatDict>("SOs/Dicts/DefaultLocDatDict");
            var sos = Resources.LoadAll<LocationData>("SOs/LocationData");
            Instance.Dict = new IdSoDict<LocationData>(sos.Length);

            foreach(var so in sos) {
                Instance.Dict.Add(so, true);

                // todo: read from json
                so.VisitTimes = 0;
                switch(so.nameid) {
                    case "lake":
                        so.trigger = new EvTrLake();
                        so.Available = true;
                        break;

                    case "witch_house":
                        so.trigger = new EvTrWitchHouse();
                        so.Available = false;
                        break;

                    case "forest":
                        so.trigger = new EvTrForest();
                        so.Available = true;
                        break;

                    case "cave":
                        so.trigger = new EvTrCave();
                        so.Available = true;
                        break;

                    case "peak":
                        so.trigger = new EvTrPeak();
                        so.Available = true;
                        break;

                    case "beacon":
                        // todo: reuse peak
                        so.trigger = null;
                        so.Available = false;
                        break;

                    case "deep_forest":
                        so.trigger = new EvTrDeepForest();
                        so.Available = false;
                        break;

                    case "ruin":
                        so.trigger = new EvTrRuin();
                        so.Available = true;
                        break;

                    case "plain":
                        so.trigger = new EvTrPlain();
                        so.Available = true;
                        break;

                    default:
                        so.trigger = null;
                        so.Available = false;
                        break;
                }
            }
        }
    }
}
