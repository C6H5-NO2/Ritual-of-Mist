using UnityEngine;

namespace ThisGame.Utils {
    public enum Weather {
        Foggy,
        Rainy,
        Sunny,
        Count
    }

    public class WeatherManager : SingletonManager<WeatherManager> {
        public delegate void NewWeather(Weather weather);

        public event NewWeather OnNewWeather;


        private Weather dayWeather;
        public Weather DayWeather {
            get => dayWeather;
            set {
                dayWeather = value;
                OnNewWeather?.Invoke(dayWeather);
            }
        }


        private void UpdateWeather(int day) {
            Weather newWeather;
            do
                newWeather = (Weather)Random.Range(0, (int)Weather.Count);
            while(DayWeather == newWeather);
            DayWeather = newWeather;
        }


        private void OnEnable() => TimeManager.Instance.OnNewDay += UpdateWeather;
        private void OnDisable() => TimeManager.Instance.OnNewDay -= UpdateWeather;
    }
}
