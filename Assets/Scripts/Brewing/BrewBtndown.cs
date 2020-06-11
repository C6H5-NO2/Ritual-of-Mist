using UnityEngine;

public class BrewBtndown : MonoBehaviour {
    private void OnMouseUpAsButton() {
        BrewingManager.Instance.ReactMessage(BrewingManager.Message.SetFire);
    }
}
