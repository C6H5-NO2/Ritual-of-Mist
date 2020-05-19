using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHandle : MonoBehaviour,
                          IPointerClickHandler,
                          IPointerEnterHandler,
                          IPointerExitHandler {
    public ItemObject ItemObj { get; set; }

    private Image itemImg;
    private Text itemCnt;

    private IEnumerator scaleRoutine;

    private void Start() {
        itemImg = transform.Find("Image").GetComponent<Image>();
        itemCnt = transform.Find("Count").GetComponent<Text>();
        itemImg.sprite = ItemObj.image;
        itemCnt.text = ItemObj.HeldCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left)
            BagManager.Instance.bagUI.FocusedSlot = this;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = SmoothScale(1.2f);
        StartCoroutine(scaleRoutine);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = SmoothScale(1);
        StartCoroutine(scaleRoutine);
    }

    private IEnumerator SmoothScale(float factor) {
        var target = new Vector3(factor, factor, 1);
        var vel = new Vector3();
        while(Vector3.Distance(itemImg.transform.localScale, target) > .001f) {
            itemImg.transform.localScale = Vector3.SmoothDamp(itemImg.transform.localScale,
                                                              target,
                                                              ref vel,
                                                              .1f);
            yield return null;
        }
    }
}
