using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThisGame.GeneralUI {
    public class GridItemControl : MonoBehaviour,
                                   IPointerClickHandler,
                                   IPointerEnterHandler,
                                   IPointerExitHandler {
        public Image itemImg;
        public Text cntText;


        public GridLayoutControl ParentControl { get; set; }


        private Items.ItemDescription itemDesc;
        public Items.ItemDescription ItemDesc {
            get => itemDesc;
            set {
                itemDesc = value;
                itemImg.sprite = itemDesc.image;
            }
        }

        private int itemCount;
        public int ItemCount {
            get => itemCount;
            set {
                itemCount = value;
                cntText.text = itemCount.ToString();
            }
        }


        public void OnPointerClick(PointerEventData eventData) {
            //if(eventData.button == PointerEventData.InputButton.Left)
            ParentControl.ClickedItem = this;
        }


        private IEnumerator scaleRoutine;

        public void OnPointerEnter(PointerEventData eventData) {
            if(!(scaleRoutine is null))
                StopCoroutine(scaleRoutine);
            scaleRoutine = SmoothScale(1.2f);
            StartCoroutine(scaleRoutine);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!(scaleRoutine is null))
                StopCoroutine(scaleRoutine);
            scaleRoutine = SmoothScale(1);
            StartCoroutine(scaleRoutine);
        }

        private IEnumerator SmoothScale(float factor) {
            var target = new Vector3(factor, factor, 1);
            var vel = new Vector3();
            var localScale = itemImg.transform.localScale;
            while(Vector3.Distance(localScale, target) > .001f) {
                localScale = Vector3.SmoothDamp(localScale, target, ref vel, .1f);
                itemImg.transform.localScale = localScale;
                yield return null;
            }
        }
    }
}
