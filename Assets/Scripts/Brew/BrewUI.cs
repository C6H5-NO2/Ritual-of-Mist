using System.Collections;
using System.Linq;
using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Brew {
    public enum PotState { BrewPrep, BrewPrg, CraftPrep }

    /// <summary> act as brew manager </summary>
    public class BrewUI : MonoBehaviour {
        public BrewSlot[] slots = new BrewSlot[3];
        public Button confirmBtn;
        public Text confirmText;

        public PlayerIcon.PotIcon potIcon;
        public GameObject brewMsgUIPrefab;


        private int SlotCount => slots.Count(slot => !slot.Empty);
        private bool IsEmpty => SlotCount == 0;
        private bool IsFull => SlotCount == 3;

        private PotState state;

        public void SetState(PotState state) {
            this.state = state;
            UpdateBtnText();
            UpdateBtn();

            potIcon.SetState(state, IsEmpty);
        }

        // ------- Handle UI Changes -------

        private void UpdateBtnText() {
            switch(state) {
                case PotState.BrewPrep:
                    confirmText.text = "酿造";
                    break;
                case PotState.BrewPrg:
                    confirmText.text = "酿造中...";
                    break;
                case PotState.CraftPrep:
                    confirmText.text = "合成";
                    break;
            }
        }

        private void UpdateBtn() {
            var count = SlotCount;
            switch(state) {
                case PotState.BrewPrep:
                    confirmBtn.interactable = count != 1;
                    break;
                case PotState.BrewPrg:
                    confirmBtn.interactable = false;
                    break;
                case PotState.CraftPrep:
                    confirmBtn.interactable = count > 1;
                    break;
            }
            confirmText.color = confirmBtn.interactable ? Color.black : new Color(.2f, .2f, .2f, 1);
        }


        // ------- Handle Drag & Drop -------

        public void OnDropToPot(ItemDescHolder holder) {
            if(state == PotState.BrewPrg)
                return;

            if(holder.Description.nameid == "bottle_solution") {
                if(state == PotState.CraftPrep) {
                    SetState(PotState.BrewPrep);
                    Destroy(holder.gameObject);
                }
            }
            else if(!IsFull) {
                Destroy(holder.gameObject);
                AddToEmptySlot(holder.Description.id);
            }
        }

        /// <remarks> check IsFull before call </remarks>
        public void AddToEmptySlot(uint id) {
            var go = itemDict[id].Instantiate(inSceneObjRef.OnTable);
            var slot = (from s in slots where s.Empty select (RectTransform)s.transform).First();
            var item = (RectTransform)go.transform;
            // see GeneralUI.ItemDrag.OnCanvasToSlot
            item.SetParent(slot);
            item.sizeDelta = slot.sizeDelta * GeneralUI.ItemDrag.ScaleRatio;
            item.anchoredPosition = Vector2.zero;
        }

        public void OnSlotItemChange() {
            UpdateBtn();

            potIcon.SetState(state, IsEmpty);
        }


        // ------- Start Brew/Craft -------

        private Craft.CraftFormula craftFormula;
        private BrewFormula formula;
        private IdSoDict<ItemDescription> itemDict;
        private InSceneObjRef inSceneObjRef;

        private void OnConfirm() {
            var (i0, i1, i2) = GetSlotItems();
            if(state == PotState.BrewPrep) {
                if(i0 == 0) {
                    // empty
                    SetState(PotState.CraftPrep);
                    return;
                }
                var product = formula.CheckIngredient(i0, i1, i2,
                                                      TimeManager.Instance.DayTime,
                                                      WeatherManager.Instance.DayWeather);
                var callback = new GameObject("BrewCallback");
                callback.AddComponent<BrewCallback>().StartBrew(product, this);
            }
            else if(state == PotState.CraftPrep) {
                if(i2 != 0) {
                    // has 3 items
                    CreateMsgUI(BrewMsgUI.MsgType.CraftFail, BrewMsgUI.MsgPos.AboveBrewUI);
                    return;
                }
                var product = craftFormula.CheckIngredient(i0, i1);
                if(product == 0) {
                    CreateMsgUI(BrewMsgUI.MsgType.CraftFail, BrewMsgUI.MsgPos.AboveBrewUI);
                    return;
                }
                foreach(var slot in slots)
                    slot.DestroyItem();
                AddToEmptySlot(product);
                CreateMsgUI(BrewMsgUI.MsgType.CraftSuc, BrewMsgUI.MsgPos.AboveBrewUI);
            }
        }

        private (uint, uint, uint) GetSlotItems() {
            var ids = (from slot in slots where !slot.Empty select slot.Item.id).ToArray();
            switch(ids.Length) {
                case 0:
                    return (0, 0, 0);
                case 1:
                    return (ids[0], 0, 0);
                case 2:
                    return (ids[0], ids[1], 0);
                default:
                    return (ids[0], ids[1], ids[2]);
            }
        }

        public void CreateMsgUI(BrewMsgUI.MsgType type, BrewMsgUI.MsgPos pos) {
            var go = Instantiate(brewMsgUIPrefab, inSceneObjRef.CustomUI, false);
            go.GetComponent<BrewMsgUI>().SetText(type, pos);
        }


        // ------- Unity Event Func -------

        private void Start() {
            craftFormula = Craft.CraftFormula.Instance;
            formula = BrewFormula.Instance;
            itemDict = ItemDescDict.Instance.Dict;
            inSceneObjRef = InSceneObjRef.Instance;

            SetState(PotState.CraftPrep);

            gameObject.SetActive(false);
        }

        private void Awake() {
            confirmBtn.onClick.AddListener(OnConfirm);
        }
    }
}
