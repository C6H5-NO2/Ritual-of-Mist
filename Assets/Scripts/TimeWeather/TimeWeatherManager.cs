using UnityEngine;

public class TimeWeatherManager : MonoBehaviour {
    public static TimeWeatherManager Instance { get; private set; }

    // ******** Time ********

    public const float SecPerDay = 60;

    public float DayTime { get; private set; }

    private bool pause;
    public bool Pause {
        get => pause;
        set {
            if(pause == value)
                return;
            pause = value;
            if(pause) {
                accumulatedTime += Time.time - refTimePoint;
                DayTime = accumulatedTime / SecPerDay;
            }
            else {
                refTimePoint = Time.time;
            }
        }
    }

    private float accumulatedTime, refTimePoint;

    // ******** Weather ********

    public enum Weather {
        Foggy,
        Rainy,
        Sunny,
        WeatherNum
    }

    public Weather DayWeather { get; private set; } = Weather.WeatherNum;

    // ******** Day ********

    public int DayNum { get; private set; }

    public delegate void NewDay();

    public static event NewDay OnNewDay;

    private void OnNewDayInternal() {
        DayTime = 0;
        accumulatedTime = 0;
        refTimePoint = Time.time;

        Weather newWeather;
        do
            newWeather = (Weather)Random.Range(0, (int)Weather.WeatherNum);
        while(newWeather == DayWeather);
        DayWeather = newWeather;

        ++DayNum;

        OnNewDay?.Invoke();
    }

    // ******** Event Func ********

    private void Awake() {
        if(!(Instance is null)) {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start() {
        OnNewDayInternal();
    }

    private void Update() {
        if(pause)
            return;
        DayTime = (Time.time - refTimePoint + accumulatedTime) / SecPerDay;
        if(DayTime >= 1)
            OnNewDayInternal();
    }
}
