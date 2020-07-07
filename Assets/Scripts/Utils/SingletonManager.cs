using UnityEngine;

namespace Utils {
    public class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T> {
        public static T Instance { get; private set; }

        private void Awake() {
            if(Instance != null)
                Destroy(this);
            else
                Instance = (T)this;
        }
    }
}
