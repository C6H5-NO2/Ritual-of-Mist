using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Picbook {
    public class PicbookClick : MonoBehaviour, IPointerClickHandler {
        public PicbookUI picbookUI;

        public void OnPointerClick(PointerEventData eventData) {
            picbookUI.gameObject.SetActive(true);
        }
    }
}
