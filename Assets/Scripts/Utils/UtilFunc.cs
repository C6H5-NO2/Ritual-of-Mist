using UnityEngine;

namespace ThisGame.Utils {
    public static class UiltFunc {
        public static void RandPosDelta(Transform trans, float xFactor, float yFactor) {
            var x = Random.Range(-xFactor, xFactor);
            var y = Random.Range(-yFactor, yFactor);
            trans.localPosition += new Vector3(x, y, 0);
        }


        public static bool GameEnded() => Adventure.EvDatDict.Instance.Dict["ruin_fin"].TriggeredTimes > 0;
    }
}
