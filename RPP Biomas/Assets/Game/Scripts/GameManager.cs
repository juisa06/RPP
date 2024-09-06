using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;
    private bool isPaused = false;
    public int totalmedals = 0;
    public int LevelAtual = 1;
    private int lastMedalCount = 1;
    public int LifePlayer = 3;
    
    private bool isPlayerDead = false; 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isPlayerDead)
        {
            totalmedals = MedalManager.MedalsCount;
            CheckForLevelUp();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        if (LifePlayer <= 0)
        {
            Die();
        }
    }

    private void CheckForLevelUp()
    {
        if (totalmedals > lastMedalCount)
        {
            lastMedalCount = totalmedals;
            LevelAtual++;
            LoadNextLevel();
        }
    }

    private void Die()
    {
        if (isPlayerDead) return;
        isPlayerDead = true;
        lastMedalCount = totalmedals;
        SceneManager.LoadScene("Level" + LevelAtual);
        LifePlayer = 3;
        Player.Instance.transform.position = new Vector3(0, 0, 0);
        TogglePause();
        isPlayerDead = false; 
    }

    private void LoadNextLevel()
    {
        string nextSceneName = "Level" + LevelAtual;
        SceneManager.LoadScene(nextSceneName);
        Player.Instance.transform.position = new Vector3(0, 0, 0);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
