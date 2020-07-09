using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvPrgUI : MonoBehaviour {
        public Text locName;
        public Image locImg;
        public Slider prgSlider;
        public Button finBut;


        private bool started;
        private LocationData location;

        public void StartAdv(LocationData data,
                             Dictionary<Items.ItemDescription, uint> items,
                             TimeWeather timeweather) {
            if(started)
                return;
            started = true;

            location = data;
            locName.text = location.name;
            locImg.sprite = location.image;
            location.Visit();

            StartCoroutine(CountDown());

            // todo: consider using co-routine / multi-thread
            var (states, loots) = location.ProcessEvents(items, timeweather);
            this.loots = loots;
            if(states is null || loots is null) {
                events = null;
                return;
            }
            events = new List<LocationEvent>(location.events.Length);
            foreach(var ev in location.events) {
                if(states[ev])
                    events.Add(ev);
            }
        }


        private List<LocationEvent> events;
        private Dictionary<Items.ItemDescription, uint> loots;

        private void OnAdvFin() {
            Destroy(gameObject);
            if(events is null || loots is null)
                return;

            // todo: pass param
            Debug.Log("Adv in: " + location.name + " time " + location.VisitTimes);
            var sb = new StringBuilder("Events:\n");
            foreach(var ev in events)
                sb.AppendLine(ev.name);
            Debug.Log(sb);
            sb = new StringBuilder("Loots:\n");
            foreach(var lt in loots)
                sb.AppendLine(lt.Key.name + ": " + lt.Value);
            Debug.Log(sb);
        }


        private float timePast;

        private IEnumerator CountDown() {
            timePast = 0;
            while(timePast < location.timeCost) {
                prgSlider.value = timePast / location.timeCost;
                timePast += Time.deltaTime; // todo: use custom time manager (?)
                yield return null;
            }
            prgSlider.gameObject.SetActive(false);
            finBut.gameObject.SetActive(true);
        }


        private void Awake() {
            prgSlider.value = 0;
            prgSlider.gameObject.SetActive(true);
            finBut.onClick.AddListener(OnAdvFin);
            finBut.gameObject.SetActive(false);
        }
    }
}
