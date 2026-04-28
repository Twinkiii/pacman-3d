using UnityEngine;


[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{


    
    public static T Instance { get; private set; }

    public void Init()
    {
        if (Instance != null) 
        { 
            Destroy(this); 
            return; 
        }
        Instance = this as T;
    }
}
