using UnityEngine;

public class ClockIcon : MonoBehaviour {
    public Sprite[] weatherIcons;

    private Transform hand;

    private SpriteRenderer weather;
    private int currWeather;

    private void NextWeather() {
        int idx;
        do
            idx = Random.Range(0, weatherIcons.Length);
        while(idx == currWeather);
        weather.sprite = weatherIcons[currWeather = idx];
    }

    private void Awake() {
        hand = transform.Find("hand");

        weather = transform.Find("weather").GetComponent<SpriteRenderer>();
        currWeather = -1;
    }

    private void Update() {
        //hand.localRotation = Quaternion.Euler(0, 0, Time.deltaTime * -360 / TimeManager.SecPerDay) * hand.localRotation;
        hand.localRotation = Quaternion.Euler(0, 0, TimeManager.Instance.DayTime * -360);
    }

    private void OnEnable() {
        TimeManager.OnNewDay += NextWeather;
    }

    private void OnDisable() {
        TimeManager.OnNewDay -= NextWeather;
    }
}
