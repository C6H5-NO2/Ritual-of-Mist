using UnityEngine;

[CreateAssetMenu(fileName = "LocationData", menuName = "Locations/Location Description")]
public class LocationData : ScriptableObject {
    public string locName;
    [TextArea] public string locText;
    public int locCost;
    public Sprite locImage;
    public Vector2 locSybolPos;
}
