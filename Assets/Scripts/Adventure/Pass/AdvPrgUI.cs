using System.Collections;
using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
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

            // todo: delete debug func
            DebugLog(time, weather, hand);
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


        // todo: delete debug func
        private void DebugLog(Zeit time, Weather weather, (uint, uint, uint) hand) {
            var sb = new System.Text.StringBuilder(128);
            sb.AppendLine("----------------")
              .AppendLine($"Start adventure in {location.name}")
              .AppendLine($"Time: {time}  Weather: {weather}")
              .Append("Hand: ");

            var itemDict = Items.ItemDescDict.Instance.Dict;
            for(var i = 0; i < 3; ++i)
                if(hand.At(i) != 0)
                    sb.Append(itemDict[hand.At(i)].name + "  ");
            sb.AppendLine();

            sb.Append("Events: ");
            for(var i = 0; i < mask.Length; ++i)
                if(mask[i])
                    sb.Append(location.events[i].name + "  ");
            sb.AppendLine();

            sb.Append("Loots: ");
            foreach(var loot in loots)
                sb.Append(itemDict[loot].name + "  ");

            sb.AppendLine("\n----------------\n");
            Debug.Log(sb.ToString());
        }
    }
}
