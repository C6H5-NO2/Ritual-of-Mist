using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.PlayerIcon {
    public class ClockIcon : MonoBehaviour {
        public Transform hand;
        public SpriteRenderer weatherRenderer;

        // In alphabetical order, correspond to Utils.Weather
        public Sprite[] weatherIcons;

        private void UpdateIcon(Weather weather) {
            weatherRenderer.sprite = weatherIcons[(int)weather];
        }

        private void Update() {
            hand.localRotation = Quaternion.Euler(0, 0, TimeManager.Instance.DayTime * -360);
        }

        private void OnEnable() => WeatherManager.Instance.OnNewWeather += UpdateIcon;
        private void OnDisable() => WeatherManager.Instance.OnNewWeather -= UpdateIcon;
    }
}
