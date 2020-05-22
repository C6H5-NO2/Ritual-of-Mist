using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMove : MonoBehaviour {
    public ItemObject thisItem;

    //public bool ForceScriptControl { get; set; }

    public bool ForceStopDrag { get; set; }

    private Vector3 sceneWorldOffset;
    private bool isUnderUI;

    private void OnMouseDown() {
        isUnderUI = EventSystem.current.IsPointerOverGameObject();
        if(isUnderUI)
            return;
        ForceStopDrag = false;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        sceneWorldOffset = screenPos - Input.mousePosition;
    }

    private void OnMouseDrag() {
        if(isUnderUI || ForceStopDrag)
            return;
        var newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition + sceneWorldOffset);
        newpos.z = 0;
        transform.position = newpos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "bag" && isActiveAndEnabled) {
            BagManager.Instance.AddItem(thisItem);
            Destroy(gameObject);
        }
    }
}
