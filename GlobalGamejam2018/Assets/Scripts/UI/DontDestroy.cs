using UnityEngine;
using System.Collections;

/// <summary>
/// This component makes sure the GameObject that its attached to will not be destroyed
/// </summary>
public class DontDestroy : MonoBehaviour
{
	void Start()
	{
	    GameObject[] objects = GameObject.FindGameObjectsWithTag("UI");

	    foreach (var obj in objects)
	    {
	        if (obj != this.gameObject)
	            Destroy(obj);
            else
                DontDestroyOnLoad(this.gameObject);
	    }
	}
}
