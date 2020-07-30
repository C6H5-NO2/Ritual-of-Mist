using UnityEngine;

namespace ThisGame.Utils {
    /// <remarks>REMEMBER NOT to simply override awake</remarks>
    public class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T> {
        public static T Instance { get; private set; }

        protected virtual void OnInstanceAwake() { }

        protected virtual void Awake() {
            if(Instance == null) {
                Instance = (T)this;
                OnInstanceAwake();
            }
            else
                Destroy(this);
        }
    }
}
