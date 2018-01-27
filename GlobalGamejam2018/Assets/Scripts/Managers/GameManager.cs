using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game manager properties")]
    public float gameTime = 60.0f;
    public bool hasStarted = false;

    [Header("UI elements")]
    [SerializeField] private Text m_TimerText = null;

    private float m_GameTimeCounter = 0.0f;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_GameTimeCounter = gameTime;
        hasStarted = true;
    }

    private void Update()
    {
        if (!hasStarted)
            return;

        m_GameTimeCounter -= Time.deltaTime;


        if (m_GameTimeCounter <= 0)
        {
            hasStarted = false;
            m_GameTimeCounter = 0.0f;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        //int minutes = (int)m_GameTimeCounter / 60;
        int seconds = (int)m_GameTimeCounter % 60;
        float fraction = m_GameTimeCounter * 1000;
        fraction = fraction % 1000;

        m_TimerText.text = string.Format("{0:00}:{1:000}", seconds, fraction);
    }
}


