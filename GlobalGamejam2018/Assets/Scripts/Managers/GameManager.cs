using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonInstance<GameManager>
{
    [Header("Game manager properties")]
    public float gameTime = 60.0f;
    public int score = 0;
    public bool gameHasStarted { private set; get; }

    [Header("UI elements")]
    [SerializeField] private Text m_TimerText = null;
    [SerializeField] private Text m_ScoreText = null;
    [SerializeField] private GameObject m_GameUI = null;

    
    private float m_GameTimeCounter = 0.0f;
    private bool m_PanicHasStarted = false;

    private void Start()
    {
        GetComponentInChildren<SpawnManager>().PopulateWorld();
    }

    public void StartGame()
    {
        Init();        
    }

    public void Init()
    {
        m_GameTimeCounter = gameTime;
        gameHasStarted = true;
        m_GameUI.SetActive(true);
    }

    public void InitiatePanic()
    {
        m_PanicHasStarted = true;

        GameObject[] allDinos = GameObject.FindGameObjectsWithTag("Dino");

        foreach (GameObject dino in allDinos)
        {
            DinoBase dinoBase = dino.GetComponent<DinoBase>();

            if(dinoBase.IsAlive())
                dinoBase.InitiatePanic();
        }
    }

    private void Update()
    {
        if (!gameHasStarted)
            return;

        m_GameTimeCounter -= Time.deltaTime;


        if (m_GameTimeCounter <= 0)
        {
            gameHasStarted = false;
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

        m_ScoreText.text = "Score: " + score.ToString();
    }
}


