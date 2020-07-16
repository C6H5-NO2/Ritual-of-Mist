using UnityEngine;

namespace ThisGame.GeneralUI {
    public class DragItem : DragDropUI {
        private Vector2 originSize;


        protected override void OnDropOut() {
            base.OnDropOut();
            ((RectTransform)transform).sizeDelta = originSize;
        }


        protected override void Awake() {
            base.Awake();
            originSize = ((RectTransform)transform).sizeDelta;
        }
    }
}
