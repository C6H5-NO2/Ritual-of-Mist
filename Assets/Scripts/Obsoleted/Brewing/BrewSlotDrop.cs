using UnityEngine;

/// <remarks> add to BrewSlot </remarks>
public class BrewSlotDrop : MonoBehaviour {
    private BrewSlotDrag item;
    public BrewSlotDrag Item {
        get => item;
        set {
            item = value;
            BrewingManager.Instance.ReactMessage(value == null
                                                     ? BrewingManager.Message.CheckIfEmpty
                                                     : BrewingManager.Message.FillMaterial);
        }
    }

    public void Clear() {
        if(item)
            Destroy(item.gameObject);
        item = null;
    }
}
