using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Letter {
    public class LetterboxIcon : MonoBehaviour,
                                 IPointerClickHandler {
        public Transform letterUI;
        public SpriteRenderer icon;
        public Sprite closed, empty, full;


        private enum State { Closed, Empty, Full }

        private State iconState;

        private void SetState(State state) {
            iconState = state;
            switch(state) {
                case State.Closed:
                    icon.sprite = closed;
                    break;
                case State.Empty:
                    icon.sprite = empty;
                    StartCoroutine(ResetToClosed());
                    break;
                case State.Full:
                    icon.sprite = full;
                    StartCoroutine(ResetToClosed());
                    break;
            }
        }


        private IEnumerator ResetToClosed(float waittime = 1.2f) {
            yield return new WaitForSeconds(waittime);
            SetState(State.Closed);
        }


        public void OnPointerClick(PointerEventData eventData) {
            switch(iconState) {
                case State.Closed:
                    var hasUnread = LetterManager.Instance.ReceivedLetters.Any(letter => !letter.Read);
                    SetState(hasUnread ? State.Full : State.Empty);
                    break;
                case State.Empty:
                case State.Full:
                    letterUI.gameObject.SetActive(true);
                    break;
            }
        }


        private void Awake() {
            SetState(State.Closed);
        }
    }
}
