using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Menu {
    public class MainMenuUI : MonoBehaviour {
        public Button startBtn, creditsBtn, exitBtn;
        public GameObject creditsUI;
        // todo: exitConfirmUI

        private bool firstClick;

        private void OnEnable() {
            Utils.TimeManager.Instance.Pause = true;
            creditsUI.SetActive(false);
        }

        private void OnDisable() {
            Utils.TimeManager.Instance.Pause = false;
        }

        private void Awake() {
            // todo: load by file
            firstClick = false;

            startBtn.onClick.AddListener(delegate {
                gameObject.SetActive(false);
                if(!firstClick) {
                    startBtn.transform.GetChild(0).GetComponent<Text>().text = "继续游戏";
                    firstClick = true;
                }
            });

            creditsBtn.onClick.AddListener(delegate { creditsUI.SetActive(true); });

            exitBtn.onClick.AddListener(Application.Quit);
        }
    }
}
