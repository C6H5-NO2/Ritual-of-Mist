using UnityEngine;

namespace ThisGame.Utils {
    public enum TimeConstraint { None, Day, Night }

    // avoid naming conflict with UnityEngine.Time
    public struct Zeit {
        public Zeit(float time) => zeit = time;
        public static implicit operator float(Zeit z) => z.zeit;
        public override string ToString() => $"{zeit}";
        public bool IstTag() => zeit < .5f;
        public bool IstAbend() => zeit > .5f;
        private readonly float zeit;
    }


    [DefaultExecutionOrder(-10)]
    public class TimeManager : SingletonManager<TimeManager> {
        // ------- Time -------
        public const float SecPerDay = 60;

        /// <summary> Normalized time in range [0, 1) </summary>
        public Zeit DayTime => new Zeit((accuTime + timePast) / SecPerDay);

        public float DeltaTime => pause ? 0 : Time.deltaTime;

        private float accuTime, refPoint, timePast;

        private bool pause;
        public bool Pause {
            get => pause;
            set {
                pause = value;
                if(pause) {
                    accuTime += timePast;
                    timePast = 0;
                }
                else
                    refPoint = Time.time;
            }
        }


        // ------- Day -------
        public int DayNum { get; private set; }

        private void OnNewDayInternal() {
            accuTime = 0;
            refPoint = Time.time;
            timePast = 0;

            ++DayNum;

            OnNewDay?.Invoke(DayNum);
        }

        public delegate void NewDay(int day);

        public event NewDay OnNewDay;


        // ------- Func -------
        private void Start() {
            DayNum = 0;
            OnNewDayInternal();
        }


        private void Update() {
            if(pause)
                return;
            timePast = Time.time - refPoint;
            if(accuTime + timePast >= SecPerDay)
                OnNewDayInternal();
        }
    }
}
