using UnityEngine;

public class ItemMove : MonoBehaviour {
    public ItemObject thisItem;

    private Vector3 sceneWorldOffset;

    private void OnMouseDown() {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        sceneWorldOffset = screenPos - Input.mousePosition;
    }

    private void OnMouseDrag() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + sceneWorldOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "bag") {
            BagManager.Instance.AddNewItem(thisItem);
            Destroy(gameObject);
        }
    }
}
