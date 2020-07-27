using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class MapUI : MonoBehaviour {
        public GameObject locBtnPrefab;
        public Transform locs;
        public LocDescUI locDescUI;

        private void ReceiveLocBtnClick(LocationData loction) {
            locDescUI.Location = loction;
            locDescUI.gameObject.SetActive(true);
        }

        private void OnEnable() {
            Utils.TimeManager.Instance.Pause = true;
            locDescUI.gameObject.SetActive(false);
            foreach(var loc in LocDatDict.Instance.Dict)
                if(loc.Available) {
                    var go = Instantiate(locBtnPrefab, locs, false);
                    ((RectTransform)go.transform).anchoredPosition = loc.locSymbolPos;
                    go.GetComponent<Button>().onClick.AddListener(delegate { ReceiveLocBtnClick(loc); });
                }
        }

        private void OnDisable() {
            // todo: buffer this
            foreach(Transform loc in locs)
                Destroy(loc.gameObject);

            Utils.TimeManager.Instance.Pause = false;
        }
    }
}
