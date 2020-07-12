using UnityEngine;

namespace ThisGame.Utils {
    public class InSceneObjRef : SingletonManager<InSceneObjRef> {
        //[SerializeField] private Transform overlayUI, cameraUI, cameraItems;
        //[SerializeField] private Camera mainCamera;
        // made public to suppress unity warnings
        public Transform overlayUI, cameraUI, cameraItems;
        public Camera mainCamera;


        public Transform OverlayUI => overlayUI;
        public Transform CameraUI => cameraUI;
        public Transform CameraItems => cameraItems;
        public Camera MainCamera => mainCamera;
    }
}
