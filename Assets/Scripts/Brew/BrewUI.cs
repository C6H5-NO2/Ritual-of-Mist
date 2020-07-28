using System;
using System.Collections;
using System.Collections.Generic;
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


        private PotState state;

        public void SetState(PotState state) {
            this.state = state;
            if(state == PotState.BrewPrep)
                confirmText.text = "酿造";
            else if(state == PotState.CraftPrep)
                confirmText.text = "合成";
        }


        public bool IsFull => !(slots[0].Empty || slots[1].Empty || slots[2].Empty);


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
        private void AddToEmptySlot(uint id) {
            var go = itemDict[id].Instantiate(onTableCanvas);
            var slot = (from s in slots where s.Item == null select (RectTransform)s.transform).First();
            var item = (RectTransform)go.transform;
            // see GeneralUI.ItemDrag.OnCanvasToSlot
            item.SetParent(slot);
            item.sizeDelta = slot.sizeDelta * GeneralUI.ItemDrag.ScaleRatio;
            item.anchoredPosition = Vector2.zero;
        }


        public void OnSlotItemChange() {
            //var (i0, i1, i2) = GetSlotItems();
            //const string nan = "NAN";
            //var n0 = i0 == 0 ? nan : itemDict[i0].name;
            //var n1 = i1 == 0 ? nan : itemDict[i1].name;
            //var n2 = i2 == 0 ? nan : itemDict[i2].name;
            //Debug.Log($"OnSlotItemChange: {n0}, {n1}, {n2}");
        }


        private (uint, uint, uint) GetSlotItems() {
            var ids = (from slot in slots where slot.Item != null select slot.Item.id).ToArray();
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


        // todo: use craft table instead
        private Craft.CraftFormula craftFormula;
        private BrewFormula formula;
        private IdSoDict<ItemDescription> itemDict;
        private Transform onTableCanvas;

        private void OnConfirm() {
            switch(state) {
                case PotState.BrewPrep:
                    // todo
                    foreach(var slot in slots)
                        slot.DestroyItem();
                    Debug.Log("BrewPrep OnConfirm IsFull = " + IsFull);
                    //SetState(PotState.BrewPrg);
                    break;

                case PotState.CraftPrep:
                    var (i0, i1, i2) = GetSlotItems();
                    if(i2 != 0) {
                        // todo: show failed message
                        Debug.Log("craft failed");
                        break;
                    }
                    var product = craftFormula.CheckIngredient(i0, i1);
                    if(product == 0) {
                        // todo: show failed message
                        Debug.Log("craft failed");
                        break;
                    }
                    foreach(var slot in slots)
                        slot.DestroyItem();
                    // todo: show success message
                    Debug.Log("craft success");
                    AddToEmptySlot(product);
                    break;
            }
        }


        private void Start() {
            craftFormula = Craft.CraftFormula.Instance;
            formula = BrewFormula.Instance;
            itemDict = ItemDescDict.Instance.Dict;
            onTableCanvas = InSceneObjRef.Instance.OnTable;

            SetState(PotState.CraftPrep);

            gameObject.SetActive(false);
        }


        private void Awake() {
            confirmBtn.onClick.AddListener(OnConfirm);
        }
    }
}
