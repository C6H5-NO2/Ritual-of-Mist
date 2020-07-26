using UnityEngine;

namespace ThisGame.Utils {
    [DefaultExecutionOrder(-10)]
    public class InSceneObjRef : SingletonManager<InSceneObjRef> {
        [SerializeField] private Transform overlayUI, customUI, onTable, focus;
        [SerializeField] private Camera mainCamera;

        public Transform OverlayUI => overlayUI;
        public Transform CustomUI => customUI;
        public Transform OnTable => onTable;
        public Transform Focus => focus;
        public Camera MainCamera => mainCamera;

        private void SuppressUnityWarnings() {
            overlayUI = null;
            customUI = null;
            onTable = null;
            focus = null;
            mainCamera = null;
        }
    }


    //public class InSceneObjRef : SingletonManager<InSceneObjRef> {
    //    // SuppressUnityWarnings
    //    private Transform overlayUI, customUI, onTable, focus;
    //    private Camera mainCamera;

    //    public Transform OverlayUI => overlayUI;
    //    public Transform CustomUI => customUI;
    //    public Transform OnTable => onTable;
    //    public Transform Focus => focus;
    //    public Camera MainCamera => mainCamera;

    //    protected override void Awake() {
    //        base.Awake();
    //        overlayUI = GameObject.Find("OverlayUICanvas").transform;
    //        customUI = GameObject.Find("CustomUICanvas").transform;
    //        onTable = GameObject.Find("OnTableCanvas").transform;
    //        focus = GameObject.Find("FocusCanvas").transform;
    //        mainCamera = Camera.main;
    //    }
    //}
}
