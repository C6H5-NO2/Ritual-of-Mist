using UnityEngine;

/// <remarks> add to BrewSlot </remarks>
public class BrewSlotDrop : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        var bsdScript = other.GetComponent<BrewSlotDrag>();
        if(bsdScript is null /*|| !bsdScript.IsBeingDragged*/)
            return;
        bsdScript.DropSlot = transform;
    }

    private void OnTriggerExit2D(Collider2D other) {
        var bsdScript = other.GetComponent<BrewSlotDrag>();
        if(bsdScript is null /*|| !bsdScript.IsBeingDragged*/)
            return;
        if(bsdScript.DropSlot == transform)
            bsdScript.DropSlot = null;
    }
}
