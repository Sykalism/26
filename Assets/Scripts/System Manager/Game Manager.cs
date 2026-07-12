using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public bool isPaused {get; private set;}


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

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
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
}
