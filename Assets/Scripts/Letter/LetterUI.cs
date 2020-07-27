using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Letter {
    public class LetterUI : MonoBehaviour {
        public Text letterText;
        public Button leftBtn, rightBtn;

        private int currLetter;

        private void UpdateUI(int idx) {
            var letters = LetterManager.Instance.ReceivedLetters;
            leftBtn.interactable = idx > 0;
            rightBtn.interactable = idx < letters.Count - 1;
            if(idx < 0 || idx >= letters.Count)
                return;
            letters[idx].Read = true;
            letterText.text = letters[idx].desc;
            currLetter = idx;
        }

        private void Awake() {
            leftBtn.onClick.AddListener(delegate { UpdateUI(currLetter - 1); });
            rightBtn.onClick.AddListener(delegate { UpdateUI(currLetter + 1); });
        }

        private void OnEnable() {
            Utils.TimeManager.Instance.Pause = true;
            letterText.text = "";
            leftBtn.interactable = false;
            rightBtn.interactable = false;
            var letters = LetterManager.Instance.ReceivedLetters;
            if(letters.Count == 0)
                return;
            currLetter = letters.Count - 1;
            UpdateUI(currLetter);
        }

        private void OnDisable() {
            Utils.TimeManager.Instance.Pause = false;
        }
    }
}
