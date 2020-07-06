using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactives {
    public class BulletinIcon : MonoBehaviour {
        public GameObject adventureMap;

        private void OnMouseUpAsButton() {
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            adventureMap.SetActive(true);
        }
    }
}
