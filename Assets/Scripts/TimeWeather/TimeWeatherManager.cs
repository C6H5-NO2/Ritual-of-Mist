using UnityEngine;

// todo
public struct TimeWeather {
    public TimeWeather(float time, TimeWeatherManager.Weather weather) {
        this.time = time;
        this.weather = weather;
    }

    public bool IsDay() => time < .5f;
    public bool IsNight() => time > .5f;

    public readonly float time;
    public readonly TimeWeatherManager.Weather weather;
}


public class TimeWeatherManager : MonoBehaviour {
    public static TimeWeatherManager Instance { get; private set; }

    // ------- Time -------

    public const float SecPerDay = 60;

    /// <summary> Normalized time in range [0, 1) </summary>
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

    // ------- Weather -------

    public enum Weather {
        Foggy,
        Rainy,
        Sunny,
        WeatherNum
    }

    public Weather DayWeather { get; private set; } = Weather.WeatherNum;

    // ------- Day -------

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

    // ------- Event Func -------

    private void Awake() {
        if(Instance != null)
            Destroy(this);
        else
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
