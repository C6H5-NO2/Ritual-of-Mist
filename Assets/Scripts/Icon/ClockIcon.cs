using UnityEngine;

public class ClockIcon : MonoBehaviour {
    public Sprite[] weatherIcons;

    private Transform hand;

    private SpriteRenderer weather;

    private void UpdateWeather() {
        weather.sprite = weatherIcons[(int)TimeWeatherManager.Instance.DayWeather];
    }

    private void Awake() {
        hand = transform.Find("hand");
        weather = transform.Find("weather").GetComponent<SpriteRenderer>();
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
