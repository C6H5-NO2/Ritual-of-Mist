using UnityEngine;
using UnityEngine.EventSystems;

public class BagUIControl : MonoBehaviour {
    public GameObject bagPanelUI;

    private void Start() {
        //bagPanelUI = GameObject.Find("BagPanelUI");
        bagPanelUI.SetActive(false);
    }

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject()) return;

        bagPanelUI.SetActive(true);
        BagManager.Instance.RefreshBagUI();
    }
}
