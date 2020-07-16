using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// todo: optimize state machine
public class BrewingManager : MonoBehaviour {
    public GameObject brewPanelUI;
    public BrewSlotDrop[] slots;

    public static BrewingManager Instance { get; private set; }

    // ------- Pot State -------

    public enum State {
        Empty,
        Water,
        Potion,
        Fire,
        End
    }

    private State potState = State.Empty;
    public State PotState {
        get => potState;
        private set {
            if(potState == value)
                return;
            potState = value;
            OnPotStateChanged?.Invoke(potState);
        }
    }

    public delegate void PotStateChanged(State state);

    public static event PotStateChanged OnPotStateChanged;

    // ------- Reacting Message -------

    public enum Message {
        FillWater,
        FillMaterial,
        CheckIfEmpty,
        SetFire,
        PotIconClicked,
    }

    public void ReactMessage(Message message) {
        switch(message) {
            case Message.FillWater:
                if(potState == State.Empty) {
                    PotState = State.Water;
                    brewPanelUI.SetActive(true);
                }
                break;

            case Message.FillMaterial:
                if(potState == State.Water)
                    PotState = State.Potion;
                break;

            case Message.CheckIfEmpty:
                if(potState == State.Potion) {
                    var empty = slots.All(slot => !slot.Item);
                    if(empty)
                        PotState = State.Water;
                }
                break;

            case Message.SetFire:
                if(potState == State.Water)
                    PotState = State.Empty;
                else if(potState == State.Potion)
                    StartCoroutine(StartBrewing());
                brewPanelUI.SetActive(false);
                break;

            case Message.PotIconClicked:
                switch(potState) {
                    case State.Water:
                    case State.Potion:
                        brewPanelUI.SetActive(!brewPanelUI.activeSelf);
                        break;
                    case State.End:
                        if(product)
                            Instantiate(product.prefab,
                                        brewPanelUI.transform.position,
                                        Quaternion.identity);
                        product = null;
                        PotState = State.Empty;
                        break;
                }
                break;
        }
    }

    private IEnumerator StartBrewing() {
        PotState = State.Fire;

        BrewValidation((from slot in slots where slot.Item select slot.Item.thisItem));
        foreach(var slot in slots)
            slot.Clear();

        yield return new WaitForSeconds(5);
        PotState = State.End;
    }

    // ------- Validation -------
    // todo: move to another class

    public ItemObject brewA, brewB, brew1, brew2, brew3, brewTrash;
    private ItemObject product;

    // sh*t code
    private void BrewValidation(IEnumerable<ItemObject> materials) {
        int aCnt = 0, bCnt = 0;
        foreach(var material in materials) {
            if(material.name == brewA.name)
                ++aCnt;
            else if(material.name == brewB.name)
                ++bCnt;
        }

        product = brewTrash;

        // todo: write in config file
        var twm = TimeWeatherManager.Instance;
        var weather = twm.DayWeather;
        var normTime = twm.DayTime;
        switch(weather) {
            case TimeWeatherManager.Weather.Foggy:
                break;

            case TimeWeatherManager.Weather.Rainy:
                if(aCnt == 1 && bCnt == 2 && normTime >= .5f)
                    product = brew2;
                else if(aCnt == 2 && bCnt == 0 && normTime < .5f)
                    product = brew3;
                break;

            case TimeWeatherManager.Weather.Sunny:
                if(aCnt == 1 && bCnt == 1)
                    product = brew1;
                else if(aCnt == 2 && bCnt == 0 && normTime < .5f)
                    product = brew3;
                break;
        }
    }

    // ------- Event Func -------

    private void Awake() {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
}
