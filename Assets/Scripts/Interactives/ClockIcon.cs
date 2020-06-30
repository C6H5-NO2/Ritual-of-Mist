using UnityEngine;

public class ClockIcon : MonoBehaviour {
    public Transform hand;
    public SpriteRenderer weather;

    public Sprite[] weatherIcons;

    private void UpdateWeather() {
        //                        bug prone hack
        weather.sprite = weatherIcons[(int)TimeWeatherManager.Instance.DayWeather];
    }

    private void Update() {
        hand.localRotation = Quaternion.Euler(0, 0, TimeWeatherManager.Instance.DayTime * -360);
    }

    private void OnEnable() {
        TimeWeatherManager.OnNewDay += UpdateWeather;
    }

    private void OnDisable() {
        TimeWeatherManager.OnNewDay -= UpdateWeather;
    }
}
