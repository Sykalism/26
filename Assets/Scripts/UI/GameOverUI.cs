using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    void Start()
    {
        Hide();
    }
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            Show();
        }
        else Hide();
    }
    public void Show()
    {
        gameOverPanel.SetActive(true);
    }
    public void Hide()
    {
        gameOverPanel.SetActive(false);
        
    }
    public void ContinueButton()
    {
        Hide();
        GameManager.Instance.RestartFromLastCheckpoint();
        
        
    }
}
