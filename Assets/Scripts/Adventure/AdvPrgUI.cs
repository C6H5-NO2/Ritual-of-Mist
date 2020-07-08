using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvPrgUI : MonoBehaviour {
        public Text locName;
        public Image locImg;
        public Slider prgSlider;
        public GameObject advFinUIPrefab;

        private LocationData location;


        public void StartAdv(LocationData data, List<Items.ItemDescription> items) {
            location = data;
            locName.text = location.name;
            locImg.sprite = location.image;

            // todo: time & weather
            var tw = new TimeWeather(TimeWeatherManager.Instance.DayTime,
                                     TimeWeatherManager.Instance.DayWeather);

            StartCoroutine(CountDown());

            // todo: consider using coro
            // todo: decide events
        }


        private void OnAdvFin() {
            var obj = Instantiate(advFinUIPrefab, transform.root, false);
            obj.transform.localPosition = transform.localPosition;
            var scrp = obj.GetComponent<AdvFinUI>();
            // todo: pass items and states
            Destroy(gameObject);
        }


        private float timePast;

        private IEnumerator CountDown() {
            timePast = 0;
            while(timePast < location.timeCost) {
                prgSlider.value = timePast / location.timeCost;
                timePast += Time.deltaTime; // todo: use custom time manager (?)
                yield return null;
            }
            OnAdvFin();
        }
    }
}
