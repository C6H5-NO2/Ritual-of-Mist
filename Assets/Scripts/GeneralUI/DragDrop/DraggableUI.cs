using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private Camera refCam;
        private Vector2 screenWorldOffset;

        private Transform initialParent;


        public void OnBeginDrag(PointerEventData eventData) {
            transform.SetParent(Utils.InSceneObjRef.Instance.Focus);

            screenWorldOffset = (Vector2)refCam.WorldToScreenPoint(transform.position) - eventData.position;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = (Vector2)refCam.ScreenToWorldPoint(eventData.position + screenWorldOffset);
        }

        public void OnEndDrag(PointerEventData eventData) {
            transform.SetParent(initialParent);
        }

        private void Awake() {
            initialParent = transform.parent;
            refCam = initialParent.GetComponent<Canvas>().worldCamera;
        }
    }
}
