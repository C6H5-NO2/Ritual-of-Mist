using UnityEngine;
using UnityEngine.EventSystems;

public class PotIcon : MonoBehaviour {
    public Sprite[] potSprites;
    public GameObject brewPanelUI;

    private new SpriteRenderer renderer;

    private void UpdateSprite(BrewingManager.State state) {
        renderer.sprite = potSprites[(int)state];
    }

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        brewPanelUI.SetActive(false);
    }

    private void OnEnable() {
        BrewingManager.OnPotStateChanged += UpdateSprite;
    }

    private void OnDisable() {
        BrewingManager.OnPotStateChanged -= UpdateSprite;
    }

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        brewPanelUI.SetActive(!brewPanelUI.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGobj = other.gameObject;
        if(!otherGobj.activeInHierarchy || !otherGobj.CompareTag("MovableItem"))
            return;
        var otherItem = otherGobj.GetComponent<Dragable>().thisItem;

        // bug prone: hard coded string
        if(otherItem.name == "Large Bottle") {
            // todo: minus count???
            BrewingManager.Instance.FillWater();
            brewPanelUI.SetActive(true);
        }

        Destroy(otherGobj);
    }
}
