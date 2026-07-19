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
    [SerializeField] Button SaveSlotButton;
    [SerializeField] Button LoadSlotButton;

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
            if (!GameManager.Instance.IsPaused) PauseButton();
            else
            {
                panelStack.Back();
                if (panelStack.panels.Count == 0) ResumeButton();
            }

            if (panelStack.panels.Count == 1)
            {
                EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            }
        }
    }


    public void PauseButton()
    {
        GameManager.Instance.PauseGame();
        panelStack.Open(optionPanel);

        
    }
    public void ResumeButton()
    {
        GameManager.Instance.ResumeGame();
        panelStack.Back();
    }
    public void OpenSaveDataSlot()
    {
        panelStack.Open(saveDataSlotPanel);
        EventSystem.current.SetSelectedGameObject(SaveSlotButton.gameObject);
    }
    public void OpenLoadDataSlot()
    {
        panelStack.Open(loadDataSlotPanel);
        EventSystem.current.SetSelectedGameObject(LoadSlotButton.gameObject);
    }
    public void SaveButton(int slot)
    {
        SaveManager.Instance.Save(slot);
    }
    public void LoadButton(int slot)
    {
        SaveManager.Instance.Load(slot);
    }
    public void ExitButton()
    {
        GameManager.Instance.ExitGame();
    }
}
