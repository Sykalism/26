using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] Button resumeButton;


    void Start()
    {
        optionPanel.SetActive(false);
    }
    void Update()
    {
        ToggleOptionPanel();
    }
    public void PauseButton()
    {
        GameManager.Instance.PauseGame();
        optionPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }
    public void ResumeButton()
    {
        GameManager.Instance.ResumeGame();
        optionPanel.SetActive(false);
    }
    public void ExitButton()
    {
        GameManager.Instance.ExitGame();
    }

    private void ToggleOptionPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
            {
                ResumeButton();
            }
            else PauseButton();
        }
    }

    
}
