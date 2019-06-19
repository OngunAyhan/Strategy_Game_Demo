using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameView : MonoBehaviour
{

    [Header("Game Panels")]
    public GameObject ProtectorPanel;//Panel must be enabled if a structure is dragging. Avoid clicking buttons while dragging structure
    public GameObject BarracksPanel;// Information Panel of Barracks 
    public GameObject PowerPlantPanel;// Information Panel of Power Plant
    public GameObject CooldownPanel;// Panel used for giving a little cooldown when spawning soldiers
    

    [Header("Gameplay Buttons")]
    [SerializeField]
    private List<Button> BarracksButtons = new List<Button>();
    [SerializeField]
    private List<Button> PPButtons = new List<Button>();
    [SerializeField]
    private Button Soldier_Button;
    [SerializeField]
    private Button PowerPanelPanelCloseButton;
    [SerializeField]
    private Button BarracksPanelCloseButton;

    GameController GameController;

    void Awake()
    {
        GameController = GetComponent<GameController>();
        
        Soldier_Button.onClick.AddListener(GameController.SoldierButton);//
        BarracksPanelCloseButton.onClick.AddListener(CloseBarracksPanel);//
        PowerPanelPanelCloseButton.onClick.AddListener(ClosePowerPlantPanel);

    }

    private void Start()
    {
        SetBarracksButtonFunctions();
        SetPPButtonFunctions();
    }

    public void SetBarracksButtonFunctions()//Set the buton functions
    {
        foreach (Button button in BarracksButtons)
        {
            button.onClick.AddListener(GameController.BarracksButton);
        }
    }
    public void SetPPButtonFunctions()
    {
        foreach (Button button in PPButtons)
        {
            button.onClick.AddListener(GameController.PowerPlantButton);
        }
    }


    public void CloseBarracksPanel()
    {
        BarracksPanel.SetActive(false);
        GameController.Selected = false;
    }

    public void ClosePowerPlantPanel()
    {
        PowerPlantPanel.SetActive(false);
        GameController.Selected = false;
    }


    private void ButtonOnClick()
    {
        Debug.Log("Bastın");
    }
}
