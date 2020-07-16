using UnityEngine;

// todo: use UI instead
/// <remarks> add to item that can be added to brew slot </remarks>
public sealed class BrewSlotDrag : Dragable {
    private static void EnSlot(Transform dstSlot, BrewSlotDrag item, Transform srcSlot) {
        item.transform.parent = dstSlot;
        item.transform.localPosition = Vector3.zero;
        if(srcSlot == null) {
            item.spr.drawMode = SpriteDrawMode.Sliced; // bug prone: dirty hack
            item.spr.size = new Vector2(.8f, .8f);
            item.spr.transform.localScale = Vector3.one;
        }
        else {
            var src = srcSlot.GetComponent<BrewSlotDrop>();
            if(src.Item == item)
                src.Item = null;
        }
        item.dropSlot = dstSlot;
        dstSlot.GetComponent<BrewSlotDrop>().Item = item;
    }

    private static void DeSlot(Transform slot, Vector3 pos) {
        var drop = slot.GetComponent<BrewSlotDrop>();
        if(drop.Item == null)
            return;
        var drag = drop.Item;
        drop.Item = null;

        drag.dropSlot = null;
        drag.transform.parent = null;
        drag.transform.position = pos;
        drag.spr.drawMode = SpriteDrawMode.Simple; // bug prone: dirty hack
        drag.spr.transform.localScale = Vector3.one;
    }

    private Transform startSlot, dropSlot; // The BrewSlot, NOT the collider.
    private Vector3 startPos;
    private bool isBeingDragged;
    private SpriteRenderer spr;

    private void Awake() {
        spr = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void OnMouseDown() {
        base.OnMouseDown();
        if(isUnderUI)
            return;
        isBeingDragged = true;
        startSlot = dropSlot;
        startPos = transform.position;
    }

    private void OnMouseUp() {
        if(isUnderUI)
            return;

        isBeingDragged = false;

        if(startSlot == null) {
            if(dropSlot == null)
                return;
            DeSlot(dropSlot, startPos);
            EnSlot(dropSlot, this, null);
        }
        else {
            if(dropSlot == null)
                DeSlot(startSlot, transform.position);
            else if(dropSlot != startSlot) {
                if(dropSlot.childCount > 1)
                    EnSlot(startSlot,
                           dropSlot.GetChild(1).GetComponent<BrewSlotDrag>(), // should be child 1
                           dropSlot);
                EnSlot(dropSlot, this, startSlot);
            }
            else
                // reset position
                transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGobj = other.gameObject;
        if(!otherGobj.activeInHierarchy || !otherGobj.CompareTag("BrewSlot") || !isBeingDragged)
            return;
        dropSlot = otherGobj.transform.parent;
    }

    private void OnTriggerExit2D(Collider2D other) {
        var otherGobj = other.gameObject;
        if(!otherGobj.activeInHierarchy || !otherGobj.CompareTag("BrewSlot") || !isBeingDragged)
            return;
        if(dropSlot == otherGobj.transform.parent)
            dropSlot = null;
    }
}
