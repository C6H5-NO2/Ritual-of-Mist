using UnityEngine;

public class ItemCraft : MonoBehaviour {
    public GameObject craftButtonPrefab;
    public GameObject craftCPrefab;

    private GameObject craftButtonInstance;

    private void OnTriggerEnter2D(Collider2D other) {
        // todo: manage by craft system
        if(!isActiveAndEnabled)
            return;
        var thisMaterial = transform.parent.gameObject;
        var otherMaterial = other.transform.parent.gameObject;
        if(thisMaterial.name.StartsWith("CraftA") && otherMaterial.name.StartsWith("CraftB")) {
            var benchmark = thisMaterial.transform.position;

            otherMaterial.transform.position = benchmark + new Vector3(0.46f, 0, 0);
            otherMaterial.GetComponent<ItemMove>().ForceStopDrag = true;
            thisMaterial.GetComponent<ItemMove>().ForceStopDrag = true;

            craftButtonInstance = Instantiate(craftButtonPrefab,
                                              benchmark + new Vector3(0.23f, 0.82f, 0),
                                              Quaternion.identity);
            var scrp = craftButtonInstance.GetComponent<CraftButtonDown>();
            scrp.RawMaterial = new[] {thisMaterial, otherMaterial};
            scrp.EndProduct = craftCPrefab;
            scrp.TargetPos = benchmark + new Vector3(0.23f, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // todo: manage by craft system
        if(!isActiveAndEnabled)
            return;
        if(transform.parent.name.StartsWith("CraftA") && other.transform.parent.name.StartsWith("CraftB")) {
            Destroy(craftButtonInstance);
        }
    }
}
