using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        Instance = this.GetComponent<T>();
    }

    protected virtual void Init() {}
}
