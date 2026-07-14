using UnityEngine;
using TMPro;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] GameObject playerMenu;
    [SerializeField] CharacterBaseData sourceData;

    [Header("Status")]
    [SerializeField] GameObject statusPanel;
    [SerializeField] TMP_Text MaxHealthOutput;

    [Header("Map")]
    [SerializeField] GameObject mapPanel;


    public enum Panel
    {
        none,
        status,
        map
    }

    public Panel currentPanel {get; private set;}
    private bool onPlayerMenu;

    void Update()
    {
        MaxHealthOutput.SetText(sourceData.MaxHealth.ToString());

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (onPlayerMenu)
            {
                onPlayerMenu = false;
                playerMenu.SetActive(false);
                currentPanel = Panel.none;
            }
            else
            {
                onPlayerMenu = true;
                playerMenu.SetActive(true);
                currentPanel = Panel.status;
            }
        }

        if (onPlayerMenu)
        {
            TogglePanel();
        }
        
    }

    public void StatusButton()
    {
        currentPanel = Panel.status;
    }
    public void MapButton()
    {
        currentPanel = Panel.map;
    }

    private void TogglePanel()
    {
        if (currentPanel == Panel.status)
        {
            statusPanel.SetActive(true);
        }
        else statusPanel.SetActive(false);

        if (currentPanel == Panel.map)
        {
            mapPanel.SetActive(true);
        }
        else mapPanel.SetActive(false);
    }
}
