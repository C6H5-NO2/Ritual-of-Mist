using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure {
    public class MapManager : Utils.SingletonManager<MapManager> {
        public LocationData[] datas;
        public GameObject locSymbolPrefab;
        public RectTransform avaLocs;
        public LocDescUI locDescUI;

        private bool preserveLocations;

        public void ReceiveSymbolClick(LocationData dat) {
            Debug.Log(dat.locName);
            locDescUI.gameObject.SetActive(true);
            locDescUI.Location = dat;
        }

        public void GenAllLoc() {
            foreach(var dat in datas) {
                var symbol = Instantiate(locSymbolPrefab, avaLocs, false);
                symbol.transform.localPosition = dat.locSybolPos;
                //                                                      bug prone
                symbol.GetComponent<Button>().onClick.AddListener(delegate { ReceiveSymbolClick(dat); });
            }
        }

        public void ClearAllLoc() {
            var gridChildren = new List<GameObject>(from Transform child in avaLocs select child.gameObject);
            gridChildren.ForEach(Destroy);
        }

        private void OnEnable() {
            locDescUI.gameObject.SetActive(false);
        }

        private void OnDisable() {
            if(!preserveLocations)
                ClearAllLoc();
        }

        private void Start() {
            locDescUI.gameObject.SetActive(false);
            preserveLocations = true;
            GenAllLoc();
        }
    }
}
