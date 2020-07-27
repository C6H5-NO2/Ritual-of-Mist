using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Adventure {
    /// <summary> add to MapUI > Map </summary>
    public class MapClick : MonoBehaviour, IPointerClickHandler {
        private MapUI mapUI;

        public void OnPointerClick(PointerEventData eventData) {
            mapUI.locDescUI.gameObject.SetActive(false);
        }

        private void Awake() {
            mapUI = transform.parent.GetComponent<MapUI>();
        }
    }
}
