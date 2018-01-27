using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// The starting options for the main menu
/// </summary>
public class StartOptions : MonoBehaviour
{                                                                                                                                           
	public int sceneToStart = 1;										
	public bool changeScenes;											
	public bool changeMusicOnStart;										


	[HideInInspector] public bool inMainMenu = true;					
    public Animator animColorFade; 					
	public Animator animMenuAlpha;					
	public AnimationClip fadeColorAnimationClip;
	public AnimationClip fadeAlphaAnimationClip;

	private PlayMusic _playMusic;										
	private float _fastFadeIn = .01f;									
	private ShowPanels _showPanels;										


	/// <summary>
    /// Standard awake
    /// </summary>
	void Awake()
	{
		_showPanels = GetComponent<ShowPanels> ();

		_playMusic = GetComponent<PlayMusic> ();

#if UNITY_ANDROID || UNITY_IOS
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
    }

    /// <summary>
    /// Triggers when the start button is clicked
    /// </summary>
	public void StartButtonClicked()
	{
		if (changeMusicOnStart) 
		{
			_playMusic.FadeDown(fadeColorAnimationClip.length);
		}

		if (changeScenes) 
		{
			Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

			animColorFade.SetTrigger ("fade");
		} 

		else 
		{
			StartGameInScene();
		}

	}

    /// <summary>
    /// Called when the UI is enabled
    /// </summary>
    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneWasLoaded;
    }

    /// <summary>
    /// Called when the UI is Disabled
    /// </summary>
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneWasLoaded;
    }

    /// <summary>
    /// Triggers when an new scene is loaded
    /// </summary>
    /// <param name="scene">Scene to load</param>
    /// <param name="mode"></param>
    void SceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
		if (changeMusicOnStart)
		{
			_playMusic.PlayLevelMusic ();
		}	
	}

    /// <summary>
    /// Loads the scene but delayed
    /// </summary>
	public void LoadDelayed()
	{
		inMainMenu = false;

		_showPanels.HideMenu ();

		SceneManager.LoadScene (sceneToStart);
	}

    /// <summary>
    /// Hides menu but delayed
    /// </summary>
	public void HideDelayed()
	{
		_showPanels.HideMenu();
	}
    /// <summary>
    /// Start the game in the scene
    /// </summary>
	public void StartGameInScene()
	{
		inMainMenu = false;

		if (changeMusicOnStart) 
		{
			Invoke ("PlayNewMusic", fadeAlphaAnimationClip.length);
		}

        animMenuAlpha.SetTrigger ("fade");
		Invoke("HideDelayed", fadeAlphaAnimationClip.length);

        GameManager.Instance.StartGame();

    }


    /// <summary>
    /// Plays the new music
    /// </summary>
	public void PlayNewMusic()
	{
		_playMusic.FadeUp (_fastFadeIn);
		_playMusic.PlaySelectedMusic (1);
	}
}
