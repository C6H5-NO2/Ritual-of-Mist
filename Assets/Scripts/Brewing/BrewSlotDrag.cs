using UnityEngine;

/// <remarks> add to item that can be added to brew slot </remarks>
public sealed class BrewSlotDrag : Dragable {
    public Transform DropSlot { get; set; }
    //public bool IsBeingDragged { get; private set; }

    private static void EnSlot(Transform item, Transform slot, bool fromNull = true) {
        item.parent = slot;
        item.localPosition = Vector3.zero;
        if(fromNull) {
            var spr = item.GetComponentInChildren<SpriteRenderer>();
            spr.drawMode = SpriteDrawMode.Sliced; // bug prone: dirty hack
            spr.size = new Vector2(.8f, .8f);
            spr.transform.localScale = Vector3.one;
        }
    }

    private static void DeSlot(Transform item, Vector3 pos) {
        item.parent = null;
        item.position = pos;
        var spr = item.GetComponentInChildren<SpriteRenderer>();
        spr.drawMode = SpriteDrawMode.Simple;
        spr.transform.localScale = Vector3.one;
    }

    private Transform startSlot;
    private Vector3 startPos;

    protected override void OnMouseDown() {
        Debug.Log(Time.frameCount + gameObject.name + " enter mouse down");
        base.OnMouseDown();
        if(isUnderUI)
            return;
        //IsBeingDragged = true;
        startSlot = DropSlot;
        startPos = transform.position;
        Debug.Log(Time.frameCount + gameObject.name + " finish mouse down");
    }

    private void OnMouseUp() {
        if(isUnderUI)
            return;

        //IsBeingDragged = false;
        if(startSlot is null) {
            if(DropSlot is null)
                return;
            if(DropSlot.childCount > 0)
                DeSlot(DropSlot.GetChild(0), startPos); // should be only one child
            EnSlot(transform, DropSlot);
        }
        else {
            if(DropSlot is null) {
                DeSlot(transform, transform.position);
            }
            else if(DropSlot != startSlot) {
                if(DropSlot.childCount > 0)
                    EnSlot(DropSlot.GetChild(0), startSlot, false); // should be only one child
                EnSlot(transform, DropSlot, false);
            }
            else {
                // reset position
                transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
