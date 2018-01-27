using UnityEngine;
using System.Collections;

/// <summary>
/// Pausing logic
/// </summary>
public class Pause : MonoBehaviour
{
	private ShowPanels _showPanels;					
	private bool _isPaused;							
	private StartOptions _startScript;				                                      
	
    /// <summary>
    /// Awake
    /// </summary>
	void Awake()
	{
		_showPanels = GetComponent<ShowPanels> ();
		_startScript = GetComponent<StartOptions> ();
	}

    /// <summary>
    /// Called every frame
    /// </summary>
	void Update () {

		if (Input.GetButtonDown ("Cancel") && !_isPaused && !_startScript.inMainMenu) 
		{
			DoPause();
		} 
		else if (Input.GetButtonDown ("Cancel") && _isPaused && !_startScript.inMainMenu) 
		{
			UnPause ();
		}
	
	}


    /// <summary>
    /// Pause game
    /// </summary>
	public void DoPause()
	{
		_isPaused = true;
		Time.timeScale = 0;
		_showPanels.ShowPausePanel ();
	}

    /// <summary>
    /// Unpause the game
    /// </summary>
	public void UnPause()
	{
		_isPaused = false;
		Time.timeScale = 1;
		_showPanels.HidePausePanel ();
	}
}
