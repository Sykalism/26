using UnityEngine;
public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager Instance {get; private set;}

    [SerializeField] SaveManager saveManager;
    [SerializeField] GameOverUI gameOverUI;
    public bool IsPaused {get; private set;}
    public bool IsGameOver {get; private set;}


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
        IsGameOver = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.LoadLastSave();
        }
    }
    // get save and load data
    public void Save(SaveData data)
    {
        data.game.isGameOver = IsGameOver;
    }
    public void Load(SaveData data)
    {
        IsGameOver = data.game.isGameOver;
    }


    // public methods
    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        IsGameOver = true;
        PauseGame();
    }
    public void RestartFromLastCheckpoint()
    {
        if (IsPaused) ResumeGame();
        SaveManager.Instance.LoadLastSave();
    }
    public void ExitGame()
    {
        # if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
        # else
            Application.Quit();
        # endif
        
    }
}
