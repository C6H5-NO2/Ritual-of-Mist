using System.Collections;
using ThisGame.NewAdv;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThisGame.Picbook {
    public class PicbookLabelHolder : MonoBehaviour,
                                      IPointerClickHandler,
                                      IPointerEnterHandler,
                                      IPointerExitHandler {
        public Text nameText;
        public RectTransform labelTrans;

        public static readonly Vector2 UnfocusPos = new Vector2(43, 0);

        public PicbookLabelList ParentList { get; set; }
        public LocationData Location { get; set; }


        public void OnPointerClick(PointerEventData eventData) {
            ParentList.ClickedLabel = this;
        }


        private IEnumerator moveRoutine;

        public void OnPointerEnter(PointerEventData eventData) {
            if(!(moveRoutine is null))
                StopCoroutine(moveRoutine);
            moveRoutine = SmoothMove(Vector2.zero);
            StartCoroutine(moveRoutine);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!(moveRoutine is null))
                StopCoroutine(moveRoutine);
            moveRoutine = SmoothMove(UnfocusPos);
            StartCoroutine(moveRoutine);
        }

        private IEnumerator SmoothMove(Vector2 target) {
            var vel = new Vector2();
            var anchPos = labelTrans.anchoredPosition;
            while(Vector2.Distance(anchPos, target) > .001f) {
                anchPos = Vector2.SmoothDamp(anchPos, target, ref vel, .1f);
                labelTrans.anchoredPosition = anchPos;
                yield return null;
            }
        }


        private void Start() {
            labelTrans.anchoredPosition = UnfocusPos;
            nameText.text = Location.name;
        }
    }
}
