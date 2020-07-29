using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Brew {
    public class BrewMsgUI : MonoBehaviour {
        public enum MsgType { BrewSuc, BrewFail, CraftSuc, CraftFail }

        public enum MsgPos { AbovePot, AboveBrewUI }

        public Text text;
        public Image image;

        private RectTransform rectTransform;

        public void SetText(MsgType type, MsgPos pos) {
            switch(type) {
                case MsgType.BrewSuc:
                    text.text = "酿造成功";
                    break;
                case MsgType.BrewFail:
                    text.text = "酿造失败";
                    break;
                case MsgType.CraftSuc:
                    text.text = "合成成功";
                    break;
                case MsgType.CraftFail:
                    text.text = "合成失败";
                    break;
            }
            switch(pos) {
                case MsgPos.AbovePot:
                    rectTransform.anchoredPosition = new Vector2(-444.5f, -283);
                    break;
                case MsgPos.AboveBrewUI:
                    rectTransform.anchoredPosition = new Vector2(-444.5f, -83);
                    break;
            }
            StartCoroutine(SmoothAnim());
        }


        private IEnumerator SmoothAnim() {
            float showTime = 1.7f, moveTime = .1f;

            Vector2 posVel = new Vector2(),
                    anchPos = rectTransform.anchoredPosition,
                    posTarget = anchPos + new Vector2(0, 33);
            float colVel = 0, color = 0, colTarget = 1;
            var tm = Utils.TimeManager.Instance;
            while(Vector2.Distance(anchPos, posTarget) > .001f) {
                anchPos = Vector2.SmoothDamp(anchPos, posTarget, ref posVel, moveTime, Mathf.Infinity, tm.DeltaTime);
                rectTransform.anchoredPosition = anchPos;

                color = Mathf.SmoothDamp(color, colTarget, ref colVel, moveTime, Mathf.Infinity, tm.DeltaTime);
                text.color = new Color(1, 1, 1, color);
                image.color = new Color(1, 1, 1, color);

                yield return null;
            }

            showTime -= moveTime;
            while(showTime > 0) {
                showTime -= tm.DeltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }


        private static BrewMsgUI instance;

        private void Awake() {
            if(instance != null && instance.isActiveAndEnabled) {
                instance.StopAllCoroutines();
                Destroy(instance.gameObject);
            }
            instance = this;
            rectTransform = (RectTransform)transform;
        }
    }
}
