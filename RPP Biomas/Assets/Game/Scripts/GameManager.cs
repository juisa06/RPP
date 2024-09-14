using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;
    private bool isPaused = false;
    private int totalmedals = 0;
    public int LevelAtual = 1;
    private int lastMedalCount = 0;
    public int LifePlayer = 3;
    public int playerDamage = 1;
    public bool hasInvisiblePotion = false;
    public bool Estouinvisivel = false;

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

    void Start()
    {
        InitializeCollectiblesForCurrentLevel();
    }

    void Update()
    {
        if (!isPlayerDead)
        {
            totalmedals = MedalManager.MedalsCount; // Certifique-se de que MedalManager está corretamente implementado
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

    private void InitializeCollectiblesForCurrentLevel()
    {
        // Defina os itens da fase atual
        string[] phaseItems = GetCurrentLevelCollectibles();
        CollectiblesManager.Instance.InitializePhaseItems(phaseItems);
    }

    private string[] GetCurrentLevelCollectibles()
    {
        // Exemplo de como você pode definir os itens da fase atual
        switch (LevelAtual)
        {
            case 1:
                return new string[] { "Animal1", "RareStone1", "PuzzlePiece1" };
            case 2:
                return new string[] { "Animal2", "RareStone2", "PuzzlePiece2" };
            // Continue para os outros níveis
            default:
                return new string[] { };
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

        if (CollectiblesManager.Instance != null)
        {
            CollectiblesManager.Instance.ResetCurrentPhaseCollectibles(); // Apenas reseta os itens da fase atual
        }

        SceneManager.LoadScene("Level" + LevelAtual);
        LifePlayer = 3;
        TogglePause();
        isPlayerDead = false; 
    }

    private void LoadNextLevel()
    {
        string nextSceneName = "Level" + LevelAtual;
        SceneManager.LoadScene(nextSceneName);
        InitializeCollectiblesForCurrentLevel(); // Inicializa os colecionáveis do próximo nível
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
