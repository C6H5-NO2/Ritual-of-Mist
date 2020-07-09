using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour {
    public ItemObject thisItem;

    protected bool isUnderUI;
    protected Vector3 sceneWorldOffset;

    protected virtual void OnMouseDown() {
        isUnderUI = EventSystem.current.IsPointerOverGameObject();
        if(isUnderUI)
            return;
        sceneWorldOffset = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
    }

    protected virtual void OnMouseDrag() {
        if(isUnderUI)
            return;
        var newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition + sceneWorldOffset);
        newpos.z = 0;
        transform.position = newpos;
    }
}
