using UnityEngine;
using UnityEngine.EventSystems;

public class PotIcon : MonoBehaviour {
    public Sprite[] potSprites;

    private SpriteRenderer spr;

    private void UpdateSprite(BrewingManager.State state) {
        spr.sprite = potSprites[(int)state];
    }

    private void Awake() {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        BrewingManager.OnPotStateChanged += UpdateSprite;
    }

    private void OnDisable() {
        BrewingManager.OnPotStateChanged -= UpdateSprite;
    }

    private void OnMouseUpAsButton() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        BrewingManager.Instance.ReactMessage(BrewingManager.Message.PotIconClicked);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGobj = other.gameObject;
        if(!otherGobj.activeInHierarchy || !otherGobj.CompareTag("MovableItem"))
            return;
        var otherItem = otherGobj.GetComponent<Dragable>().thisItem;

        // bug prone: hard coded string
        if(otherItem.name == "Large Bottle") {
            BrewingManager.Instance.ReactMessage(BrewingManager.Message.FillWater);
            // todo (?): minus count
            // todo (?): check if already filled
            Destroy(otherGobj);
        }
    }
}
