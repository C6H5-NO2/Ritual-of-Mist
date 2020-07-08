using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Utils {
    public class DragableUI : MonoBehaviour,
                              IBeginDragHandler,
                              IDragHandler,
                              IEndDragHandler {
        protected Camera RefCam { get; private set; }
        private Vector2 screenWorldOffset;

        public virtual void OnBeginDrag(PointerEventData eventData) {
            screenWorldOffset = (Vector2)RefCam.WorldToScreenPoint(transform.position) - eventData.position;
        }

        public virtual void OnDrag(PointerEventData eventData) {
            transform.position = (Vector2)RefCam.ScreenToWorldPoint(eventData.position + screenWorldOffset);
        }

        public virtual void OnEndDrag(PointerEventData eventData) { }

        protected virtual void Awake() {
            RefCam = transform.root.GetComponent<Canvas>().worldCamera;
        }
    }
}
