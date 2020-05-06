using UnityEngine;

public class BagControl : MonoBehaviour {
    private GameObject bagPanelUI;

    private void Start() {
        bagPanelUI = GameObject.Find("BagPanelUI");
    }

    private void OnMouseDown() {
        bagPanelUI.SetActive(true);
    }
}
