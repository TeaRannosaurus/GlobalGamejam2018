using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Music playing manager
/// </summary>
public class PlayMusic : MonoBehaviour
{
	public AudioClip titleMusic;					
	public AudioClip mainMusic;						
	public AudioMixerSnapshot volumeDown;			
	public AudioMixerSnapshot volumeUp;				


	private AudioSource _musicSource;				
	private float _resetTime = .01f;				                      


	void Awake () 
	{
		_musicSource = GetComponent<AudioSource> ();
	}


    /// <summary>
    /// Play the level music
    /// </summary>
	public void PlayLevelMusic()
	{
		switch (SceneManager.GetActiveScene().buildIndex)
		{
			case 0:
				_musicSource.clip = titleMusic;
				break;
			case 1:
				_musicSource.clip = mainMusic;
				break;
		}
		FadeUp (_resetTime);
		_musicSource.Play ();
	}

	/// <summary>
    /// Play the selected music
    /// </summary>
    /// <param name="musicChoice">Track number</param>
	public void PlaySelectedMusic(int musicChoice)
	{

		switch (musicChoice) 
		{
		case 0:
			_musicSource.clip = titleMusic;
			break;
		case 1:
			_musicSource.clip = mainMusic;
			break;
		}
		_musicSource.Play ();
	}

    /// <summary>
    /// Fades in the volume
    /// </summary>
    /// <param name="fadeTime">Time to fade</param>
	public void FadeUp(float fadeTime)
	{
		volumeUp.TransitionTo (fadeTime);
	}

    /// <summary>
    /// Fades out the volume
    /// </summary>
    /// <param name="fadeTime">Time to fade</param>
	public void FadeDown(float fadeTime)
	{
		volumeDown.TransitionTo (fadeTime);
	}
}
