using UnityEngine;
using UnityEngine.EventSystems;

public class LettershelfIcon : MonoBehaviour {
    public GameObject letterPanelUI;

    //private void Start() {
    //    letterPanelUI.SetActive(false);
    //}

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        letterPanelUI.SetActive(true);
    }
}
