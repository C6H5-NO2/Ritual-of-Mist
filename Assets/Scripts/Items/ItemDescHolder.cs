using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Items {
    // todo: generate from script in release
    public class ItemDescHolder : MonoBehaviour {
        public ItemDescription Description; // { get; set; }

        public void UpdateDesc() {
            gameObject.name = Description.nameid;
            var img = GetComponent<Image>();
            if(img != null) {
                img.sprite = Description.image;
                img.color = Color.white;
                img.SetNativeSize();
            }
        }

        private void Awake() {
            if(Description != null)
                UpdateDesc();
        }
    }
}
