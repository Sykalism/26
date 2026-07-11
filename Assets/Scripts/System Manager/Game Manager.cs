using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public bool isPaused {get; private set;}

    [SerializeField] GameObject optionPanel;


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
        optionPanel.SetActive(false);
    }
    void Update()
    {
        ToggleOption();
    }
    public void PauseGame()
    {
        isPaused = true;
        optionPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        # if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
        # else
            Application.Quit();
        # endif
        
    }

    private void ToggleOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else PauseGame();
        }
    }
}
