using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    public class ClickClose : MonoBehaviour, IPointerClickHandler {
        public bool destroy;
        private Transform target;

        public void OnPointerClick(PointerEventData eventData) {
            if(destroy)
                Destroy(target.gameObject);
            else
                target.gameObject.SetActive(false);
        }

        private void Awake() {
            target = transform.parent;
        }
    }
}
