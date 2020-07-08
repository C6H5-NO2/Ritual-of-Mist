﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Adventure {
    public class DefaultDropUI : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            var dragUI = eventData.pointerDrag.GetComponent<DefaultDragUI>();
            if(dragUI != null)
                dragUI.DropSlot = transform;
        }
    }
}
