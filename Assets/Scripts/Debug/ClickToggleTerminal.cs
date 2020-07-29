using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToggleTerminal : MonoBehaviour, IPointerClickHandler {
    public DebugTerminal terminal;

    public void OnPointerClick(PointerEventData eventData) {
        terminal.gameObject.SetActive(!terminal.gameObject.activeSelf);
    }
}
