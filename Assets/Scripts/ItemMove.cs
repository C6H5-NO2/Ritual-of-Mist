using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMove : MonoBehaviour {
    public ItemObject thisItem;

    private Vector3 sceneWorldOffset;
    private bool isUnderUI;

    private void OnMouseDown() {
        if(isUnderUI = EventSystem.current.IsPointerOverGameObject()) return;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        sceneWorldOffset = screenPos - Input.mousePosition;
    }

    private void OnMouseDrag() {
        if(isUnderUI) return;
        var newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition + sceneWorldOffset);
        newpos.z = 0;
        transform.position = newpos;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "bag") {
            BagManager.Instance.AddItem(thisItem);
            Destroy(gameObject);
        }
    }
}
