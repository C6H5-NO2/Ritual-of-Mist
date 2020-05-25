using UnityEngine;

public class TimeManager : MonoBehaviour {
    public const float SecPerDay = 60;

    public static TimeManager Instance { get; private set; }

    public int DayNum { get; private set; }

    public float DayTime => accumulatedTime / SecPerDay;

    private bool pause;
    public bool Pause {
        get => pause;
        set {
            if(pause == value)
                return;
            pause = value;
            if(pause) {
                accumulatedTime += Time.time - refTimePoint;
            }
            else {
                refTimePoint = Time.time;
            }
        }
    }

    public delegate void NewDay();

    public static event NewDay OnNewDay;

    private float accumulatedTime, refTimePoint;

    private void OnNewDayInternal() {
        accumulatedTime = 0;
        refTimePoint = Time.time;
        ++DayNum;
        OnNewDay?.Invoke();
    }

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
        if(!pause)
            accumulatedTime = Time.time - refTimePoint;
        if(accumulatedTime >= SecPerDay)
            OnNewDayInternal();
    }
}
