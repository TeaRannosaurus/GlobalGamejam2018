using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// Checks for the Event system when the game loads
/// </summary>
public class EventSystemChecker : MonoBehaviour
{
    

    /// <summary>
    ///  Any static function tagged with RuntimeInitializeOnLoadMethod will be called only a single time when the game load
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitSceneCallback()
    {
        SceneManager.sceneLoaded += SceneWasLoaded;
    }

    /// <summary>
    /// Depricated function
    /// </summary>
    /// <param name="scene">Scene to load</param>
    /// <param name="mode">Load method</param>
    public static void SceneWasLoaded(Scene scene, LoadSceneMode mode)
	{
		if(!FindObjectOfType<EventSystem>())
		{
			GameObject obj = new GameObject("EventSystem");

			obj.AddComponent<EventSystem>();
			obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
		}
	}
}
