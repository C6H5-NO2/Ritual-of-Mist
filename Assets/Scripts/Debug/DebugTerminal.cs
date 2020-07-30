using System.Globalization;
using System.Linq;
using ThisGame.Bag;
using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

public class DebugTerminal : MonoBehaviour {
    private InputField inputField;


    private static void ProcessCommand(string command) {
        var tok = command.Trim().Split();
        if(tok.Length < 2)
            // error command
            return;

        var main = tok[0];
        switch(main) {
            case "giveid": {
                if(ItemDescDict.Instance.Dict.TryGetValue(tok[1], out var desc)) {
                    var count = 1;
                    if(tok.Length == 3)
                        if(!int.TryParse(tok[2], NumberStyles.Integer, null, out count))
                            // error command
                            break;
                    while(count-- > 0) {
                        var go = desc.Instantiate(InSceneObjRef.Instance.OnTable);
                        UiltFunc.RandPosDelta(go.transform, 54, 36);
                    }
                }
                //else // error command
                break;
            }

            case "give":
            case "item": {
                var desc = ItemDescDict.Instance.Dict.FirstOrDefault(d => d.name == tok[1]);
                if(desc == null)
                    // error command
                    break;
                var count = 1;
                if(tok.Length == 3)
                    if(!int.TryParse(tok[2], NumberStyles.Integer, null, out count))
                        // error command
                        break;
                while(count-- > 0) {
                    var go = desc.Instantiate(InSceneObjRef.Instance.OnTable);
                    UiltFunc.RandPosDelta(go.transform, 54, 36);
                }
                break;
            }

            case "money":
            case "gold": {
                if(tok.Length != 2)
                    // error command
                    break;
                if(int.TryParse(tok[1], NumberStyles.Integer, null, out var delta))
                    BagManager.Instance.Gold += delta;
                //else // error command
                break;
            }

            case "weather": {
                if(tok.Length != 2)
                    // error command
                    break;
                switch(tok[1]) {
                    case "foggy":
                    case "雾":
                    case "雾天":
                        WeatherManager.Instance.ForceSetWeather(Weather.Foggy);
                        break;
                    case "rainy":
                    case "雨":
                    case "雨天":
                        WeatherManager.Instance.ForceSetWeather(Weather.Rainy);
                        break;
                    case "sunny":
                    case "晴":
                    case "晴天":
                        WeatherManager.Instance.ForceSetWeather(Weather.Sunny);
                        break;
                    //default: // error command
                }
                break;
            }

            case "exit":
            case "quit":
                Application.Quit();
                break;

            //default: // error command
        }
    }


    private void OnEndEdit(string text) {
        if(!(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))) {
            // click outside
            gameObject.SetActive(false);
            return;
        }
        ProcessCommand(text);
        inputField.text = "";
        inputField.ActivateInputField();
    }


    private void OnDisable() {
        inputField.text = "";
    }


    private void Awake() {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(OnEndEdit);
        gameObject.SetActive(false);
    }
}
