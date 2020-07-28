using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Items {
    // todo: generate from script in release
    [DefaultExecutionOrder(-5)]
    public class ItemDescHolder : MonoBehaviour {
        public ItemDescription Description; // { get; set; }
        private Image image;

        public void UpdateDesc() {
            gameObject.name = Description.nameid;

            image.sprite = Description.image;
            image.color = Color.white;
            image.SetNativeSize();
        }

        private void Awake() {
            image = GetComponent<Image>();
            if(Description != null)
                UpdateDesc();
        }
    }
}
