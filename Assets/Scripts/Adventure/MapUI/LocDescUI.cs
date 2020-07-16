using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class LocDescUI : MonoBehaviour {
        public Text locName, locText, locCost;
        public Image locImg;
        public Button locConfirm;
        public GameObject advObjUIPrefab;


        private readonly Color avalible = Color.yellow,
                               unavalible = Color.red;


        private LocationData location;
        public LocationData Location {
            set {
                location = value;
                if(location == null)
                    return;
                FitPosition(location.locSymbolPos);
                locName.text = location.name;
                locText.text = location.desc;
                locCost.text = location.goldCost.ToString();
                locCost.color = Bag.BagManager.Instance.Gold >= location.goldCost ? avalible : unavalible;
                locImg.sprite = location.image;
            }
        }


        private void OnConfirm() {
            if(locCost.color == unavalible)
                return;
            Debug.Log("Start adventure in: " + location.name);
            Bag.BagManager.Instance.Gold -= location.goldCost;

            var obj = Instantiate(advObjUIPrefab, Utils.InSceneObjRef.Instance.CameraUI, false);
            Utils.UiltFunc.RandPosDelta(obj.transform, 150, 100);
            obj.GetComponent<AdvPrepUI>().Location = location;

            //transform.root.gameObject.SetActive(false);
            MapManager.Instance.gameObject.SetActive(false);
        }


        private RectTransform rectTrans;

        // todo
        private void FitPosition(Vector2 pos) {
            // wrong code
            //var padding = new Vector2(20, 11);
            //var w = Screen.width - padding.x;
            //var h = Screen.height - padding.y;
            //rectTrans.localPosition = pos;
            //// start from bottom-left, enumerate clockwise
            //rectTrans.GetWorldCorners(worldCornerBuf);
            //var min = worldCornerBuf[0];
            //var max = worldCornerBuf[2];
            //if(min.x < padding.x)
            //    pos.x += padding.x - min.x;
            //else if(max.x > w)
            //    pos.x -= max.x - w;
            //if(min.y < padding.y)
            //    pos.y += padding.y - min.y;
            //else if(max.y > h)
            //    pos.y -= max.y - h;

            //Debug.Log("rectTrans.offsetMin = " + rectTrans.offsetMin);
            //Debug.Log("rectTrans.offsetMax = " + rectTrans.offsetMax);
            rectTrans.localPosition = pos;
        }


        private void Awake() {
            rectTrans = transform as RectTransform;
            locConfirm.onClick.AddListener(OnConfirm);
        }
    }
}
