using UnityEngine;
using UnityEngine.EventSystems;

public class BagIcon : MonoBehaviour {
    public GameObject bagPanelUI;

    private void Start() {
        //bagPanelUI = GameObject.Find("BagPanelUI");
        bagPanelUI.SetActive(false);
    }

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        bagPanelUI.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGobj = other.gameObject;
        if(!otherGobj.activeInHierarchy || !otherGobj.CompareTag("MovableItem"))
            return;
        var otherItem = otherGobj.GetComponent<Dragable>().thisItem;
        BagManager.Instance.AddItem(otherItem);
        Destroy(otherGobj);
    }
}
