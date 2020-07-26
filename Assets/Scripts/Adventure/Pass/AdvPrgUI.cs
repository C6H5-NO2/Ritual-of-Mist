using System.Collections;
using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.NewAdv {
    public class AdvPrgUI : MonoBehaviour {
        public Image locImg;
        public Slider prgSlider;
        public Button finBtn;
        public GameObject advFinUIPrefab;

        private Image bg;
        private bool started;
        private LocationData location;


        public void StartAdv(LocationData location, (uint, uint, uint) hand, Zeit time, Weather weather) {
            if(started)
                return;
            started = true;

            this.location = location;
            bg.sprite = location.uibgs[1];
            locImg.sprite = location.image;
            location.OnVisit();
            StartCoroutine(CountDown());

            mask = location.trigger.Trigger(location.events, time, weather, hand, out loots);
        }


        private float timePast;

        private IEnumerator CountDown() {
            timePast = 0;
            while(timePast < location.timeCost) {
                prgSlider.value = timePast / location.timeCost;
                timePast += TimeManager.Instance.DeltaTime;
                yield return null;
            }
            prgSlider.gameObject.SetActive(false);
            finBtn.gameObject.SetActive(true);
        }


        private bool[] mask;
        private List<uint> loots;

        private void OnFin() {
            Destroy(gameObject);
            var scrp = Instantiate(advFinUIPrefab, InSceneObjRef.Instance.OverlayUI, false).GetComponent<AdvFinUI>();
            scrp.SetParams(location, mask, loots);
        }


        private void Awake() {
            bg = GetComponent<Image>();
            started = false;
            prgSlider.value = 0;
            prgSlider.gameObject.SetActive(true);
            finBtn.onClick.AddListener(OnFin);
            finBtn.gameObject.SetActive(false);
        }
    }
}
