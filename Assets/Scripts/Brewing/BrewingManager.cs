using UnityEngine;

public class BrewingManager : MonoBehaviour {
    public static BrewingManager Instance { get; private set; }

    public enum State {
        Empty,
        Water,
        Potion,
        Fire
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

    public void FillWater() {
        PotState = State.Water;
    }

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start() { }
}
