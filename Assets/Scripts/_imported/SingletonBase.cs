using UnityEngine;


namespace Pacman
{
    [DisallowMultipleComponent]
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
