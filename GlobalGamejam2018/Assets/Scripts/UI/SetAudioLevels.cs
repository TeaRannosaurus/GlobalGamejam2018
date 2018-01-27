using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Sets the audio levels
/// </summary>
public class SetAudioLevels : MonoBehaviour
{
	public AudioMixer mainMixer;					//Used to hold a reference to the AudioMixer mainMixer

    /// <summary>
    /// Sets the music level of the game
    /// </summary>
    /// <param name="musicLvl">New music sound level</param>
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

    /// <summary>
    /// Set the sfx level
    /// </summary>
    /// <param name="sfxLevel">New sfx sound level</param>
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}
}
