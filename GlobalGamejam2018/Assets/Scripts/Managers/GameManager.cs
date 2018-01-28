using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score
{
    public int position = 0;
    public int score = 0;
    public string name = "Empty name";
}


public class GameManager : SingletonInstance<GameManager>
{
    [Header("Game manager properties")]
    public float gameTime = 60.0f;
    public int score = 0;
    public bool gameHasStarted { private set; get; }
    public KeyCode endKey;

    [Header("UI elements")]
    [SerializeField] private Text m_TimerText = null;
    [SerializeField] private Text m_ScoreText = null;
    [SerializeField] private GameObject m_GameUI = null;
    [SerializeField] private GameObject m_EndGameUI = null;
    [SerializeField] private Text m_EndScoreText = null;

    private bool m_GameHasEnded = false;
    private float m_GameTimeCounter = 0.0f;
    private bool m_PanicHasStarted = false;

    private void Start()
    {
        GetComponentInChildren<SpawnManager>().PopulateWorld();
    }

    public void StartGame()
    {
        Init();
        GetComponentInChildren<SpawnManager>().GameSpawnLoop();
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

        if(Input.GetKeyDown(endKey))
            EndGame();

        m_GameTimeCounter -= Time.deltaTime;


        if (m_GameTimeCounter <= 0)
        {
            gameHasStarted = false;
            m_GameTimeCounter = 0.0f;
            if (!m_GameHasEnded)
            {
                EndGame();
            }
        }

        UpdateUI();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EndGame()
    {
        m_GameHasEnded = true;

        m_GameTimeCounter = 0;

        GameObject[] allDinos = GameObject.FindGameObjectsWithTag("Dino");

        foreach (GameObject dino in allDinos)
        {
            DinoBase dinoBase = dino.GetComponent<DinoBase>();
            dinoBase.Die(true, false);
        }

        m_EndGameUI.SetActive(true);

        m_EndScoreText.text = "Your score: " + score;

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


