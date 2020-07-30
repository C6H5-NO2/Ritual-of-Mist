using UnityEngine;

namespace ThisGame.Menu {
    public class EscDown : MonoBehaviour {
        public MainMenuUI mainMenuUI;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                var go = mainMenuUI.gameObject;
                if(!go.activeSelf)
                    go.SetActive(true);
            }
        }
    }
}
