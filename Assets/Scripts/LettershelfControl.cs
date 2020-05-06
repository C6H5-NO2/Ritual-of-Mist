using UnityEngine;

public class LettershelfControl : MonoBehaviour {
    private GameObject letterPanelUI;

    private void Start() {
        letterPanelUI = GameObject.Find("LetterPanelUI");
    }

    private void OnMouseDown() {
        letterPanelUI.SetActive(true);
    }
}
