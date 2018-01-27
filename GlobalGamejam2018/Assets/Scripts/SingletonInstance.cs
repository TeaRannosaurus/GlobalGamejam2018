using UnityEngine;
using System.Collections;

public class SingletonInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    private static bool canLazyInstantiate = true;

    protected void SetSingleton(bool allowLazyInstantiate = false)
    {
        if (instance == null)
            instance = gameObject.GetComponent<T>();

        canLazyInstantiate = allowLazyInstantiate;
    }

    public static T Instance
    {
        get
        {
            if (instance == null && canLazyInstantiate)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject holder = new GameObject();
                    holder.name = typeof(T).ToString();
                    instance = (T)holder.AddComponent(typeof(T));
                }
            }

            return instance;
        }
    }

    public static T Get
    {
        get
        {
            if (instance == null && canLazyInstantiate)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject holder = new GameObject();
                    holder.name = typeof(T).ToString();
                    instance = (T)holder.AddComponent(typeof(T));
                }
            }

            return instance;
        }
    }

    public static bool HasInstance() { return instance != null; }
}
