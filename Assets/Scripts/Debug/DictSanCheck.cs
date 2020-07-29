using System.Collections;
using System.Text;
using UnityEngine;
using ThisGame.Adventure;
using ThisGame.Items;
using ThisGame.Letter;

public class DictSanCheck : MonoBehaviour {
    //public static void TestGameobject(GameObject go) {
    //    var sb = new StringBuilder();
    //    sb.Append(go is null ? "is null; " : "not is null; ");
    //    sb.Append(go == null ? "== null; " : "!= null; ");
    //    sb.Append(go.activeSelf ? "activeSelf" : "! activeSelf");
    //    Debug.Log($"GO {go.name}: {sb}");
    //}

    //public static void TestMono(MonoBehaviour mo) {
    //    var sb = new StringBuilder();
    //    sb.Append(mo is null ? "is null; " : "not is null; ");
    //    sb.Append(mo == null ? "== null; " : "!= null; ");
    //    sb.Append(mo.isActiveAndEnabled ? "activeSelf" : "! activeSelf");
    //    Debug.Log($"MONO {mo.name}: {sb}");
    //}


    private void Start() {
        if(!ItemDescDict.Instance.Dict.ContinuityCheck(out var idx))
            Debug.Log("ItemDescDict failed at: " + idx);

        if(!LocDatDict.Instance.Dict.ContinuityCheck(out idx))
            Debug.Log("LocDatDict failed at: " + idx);

        if(!EvDatDict.Instance.Dict.ContinuityCheck(out idx))
            Debug.Log("EvDatDict failed at: " + idx);

        if(!LetterManager.Instance.Dict.ContinuityCheck(out idx))
            Debug.Log("LetterManager failed at: " + idx);

        Debug.Log("Dict check end");
    }
}
