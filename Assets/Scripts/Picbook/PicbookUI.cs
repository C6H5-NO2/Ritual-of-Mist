using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Picbook {
    public class PicbookUI : MonoBehaviour {
        public Image locImg;
        public Text locText, payText;
        public Button leftBtn, rightBtn, payBtn;
        public PicbookLabelList labelList;


        private void DisActiveChildren() {
            locImg.gameObject.SetActive(false);
            locText.gameObject.SetActive(false);
            leftBtn.gameObject.SetActive(false);
            rightBtn.gameObject.SetActive(false);
            payBtn.gameObject.SetActive(false);
        }


        private void Awake() {
            DisActiveChildren();
        }


        private void OnEnable() {
            // todo: fill slots
        }


        private void OnDisable() {
            DisActiveChildren();
        }
    }
}
