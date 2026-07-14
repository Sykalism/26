using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] UIPanel optionPanel;
    [SerializeField] UIPanel saveDataSlotPanel;
    [SerializeField] UIPanel loadDataSlotPanel;

    [Header("Buttons")]
    [SerializeField] Button resumeButton;

    private PanelStack panelStack;




    void Start()
    {
        panelStack = new PanelStack();

        optionPanel.Hide();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.isPaused) PauseButton();
            else
            {
                panelStack.Back();
                if (panelStack.panels.Count == 0) ResumeButton();
            }
        }
    }


    public void PauseButton()
    {
        GameManager.Instance.PauseGame();
        panelStack.Open(optionPanel);

        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }
    public void ResumeButton()
    {
        GameManager.Instance.ResumeGame();
        panelStack.Back();
    }
    public void OpenSaveDataSlot()
    {
        panelStack.Open(saveDataSlotPanel);
    }
    public void OpenLoadDataSlot()
    {
        panelStack.Open(loadDataSlotPanel);
    }
    public void SaveButton(int slot)
    {
        GameManager.Instance.SaveGame(slot);
    }
    public void LoadButton(int slot)
    {
        GameManager.Instance.LoadGame(slot);
    }
    public void ExitButton()
    {
        GameManager.Instance.ExitGame();
    }
}
