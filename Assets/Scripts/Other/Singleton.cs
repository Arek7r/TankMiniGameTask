using UnityEngine;

[DefaultExecutionOrder(0)]
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        // If we don't have reference to instance than this object will take control
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this) // Else this is other instance and we should destroy it!
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance != this) // Skip if instance is other than this object.
        {
#if UNITY_EDITOR
            Debug.LogError(string.Format("Is more instances of {0:F2}", instance.name));
#endif
            return;
        }
#if UNITY_EDITOR
//        Debug.Log(string.Format("Destroyed: {0:F2}", instance.name));
#endif

        instance = null;
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }
}