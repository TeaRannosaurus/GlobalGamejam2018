using UnityEngine;
using System.Collections;

/// <summary>
/// Showing and hiding of panels
/// </summary>
public class ShowPanels : MonoBehaviour
{
	public GameObject optionsPanel;							//Reference Game Object OptionsPanel 
	public GameObject optionsTint;							//Reference Game Object OptionsTint 
	public GameObject menuPanel;							//Reference Game Object MenuPanel 
	public GameObject pausePanel;							//Reference Game Object PausePanel 

    /// <summary>
    /// Show the options panel
    /// </summary>
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}
    
    /// <summary>
    /// Hide options panel
    /// </summary>
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

    /// <summary>
    /// Show the menu panel
    /// </summary>
    public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

    /// <summary>
    /// Hide options panel
    /// </summary>
    public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

    /// <summary>
    /// Show the pause panel
    /// </summary>
    public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

    /// <summary>
    /// Hide options panel
    /// </summary>
    public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);
	}
}
