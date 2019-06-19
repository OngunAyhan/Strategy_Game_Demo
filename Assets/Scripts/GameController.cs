using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;



public class GameController : Singleton //The object that contains this script should be single 
{
    GameObject AStar;// A scene object contains A Star Path(Field of 200x200 nodes) component
    AstarPath astarPath;
    public Image CooldownImage;// Image used for soldier spawn cooldown

    Vector2 mousePos; // Position of Mouse Cursor
    
    ProperBuildingScript properBuildingScript;

    GameView GameView;


    [Header("Soldier Header")]
    public GameObject SoldierPrefab; // Prefab of Soldier
    public GameObject Soldier;// Spawned soldier
    public GameObject ClickPosition; // Mouse click position
    public GameObject ClickPositionOBJ; // Object used for setting click position of soldier
    public GameObject MoveToPosAnchor; // Soldiers move pos
    public GameObject MoveToPosAnchorOBJ;// Object used for indicating the soldier's move position
    SelectSoldier SelectSoldier;
    private bool CooldownBool = true;// Boolean used for giving a little cooldown when spawning soldiers
    

    [Header("Building Header")]
    public GameObject Building; // Selected Building
    public Vector2 BuildingPosition;// Position of Selected Building in Vector2
    public bool Selected; // Bool used for detecting if a building is selected
    public bool dragging = false; // Bool used for dragging a structure while building it
    
    

    [Header("Game Objects")]
    public List<GameObject> Soldiers = new List<GameObject>();// List of all soldiers in the scene
    public List<GameObject> Buildings = new List<GameObject>();// List of all structures in the scene
    public List<GameObject> ClickPositions = new List<GameObject>();  //List of the click positions of the soldiers
    public List<GameObject> MoveToPosAnchorPoints = new List<GameObject>();


    [Header("Building Factories")]
    public BarracksFactory barracksFactory;//Barracks factory 
    public PowerPlantFactory powerPlantFactory;//Power plant factory

    

    private void Start()
    {
        GameView = GetComponent<GameView>();
        
        AStar = GameObject.FindGameObjectWithTag("AStar");
        astarPath = AStar.GetComponent<AstarPath>();
        SelectSoldier = GetComponent<SelectSoldier>();
        CooldownImage = GameView.CooldownPanel.GetComponent<Image>();
    }
    
    

    void Update()
    {

        if (Input.GetMouseButtonDown(0))//Send ray to the mouse cursor position. Detect if hit has collider, if not, call ReleaseSoldiers();  
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null)
            {
                ReleaseSoldiers();
            }

        }

        ClearMap();

        DragAndBuild();

        if (GameView.CooldownPanel.activeSelf && CooldownImage.fillAmount > 0)//Cooldown decreases when player spawned a soldier, if value equals to 0 then disable cooldown panel
        {
            
            CooldownImage.fillAmount -= Time.deltaTime;
            if (CooldownImage.fillAmount == 0)
            {
                GameView.CooldownPanel.SetActive(false);
            }
        }

    }
    
    void ClearMap()//Publish an event to destroy all th scene objects
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GetComponent<Publisher>().PublishTheEvent();
        }
    }


    //Disable Selected Sprite and set selected soldier of GameController null
    void ReleaseSoldiers()
    {
        foreach (GameObject soldier in Soldiers)
        {
            soldier.GetComponent<SoldierScript>().SelectedSprite.SetActive(false);
            SelectSoldier.SelectedSoldier = null;
        }
    }

    //Instantiated building always follows the mouse cursor while it isn't builded. If there is no overlaping, it can be set when clicked left mouse button. If clicked to right mouse while it isn't builded, it would be destroy
    //If structure is builded then it will be added to the buildings list
    void DragAndBuild()
    {

        

        if (GetComponent<ScrollScript>().IsMouseOverUI)
        {
            //
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (dragging)
            {
                
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (!properBuildingScript.denying)
                    {
                        Cursor.visible = true;
                        astarPath.Scan();
                        Building.GetComponent<ProperBuildingScript>().builded = true;
                        Buildings.Add(Building);
                        dragging = false;
                        GameView.ProtectorPanel.SetActive(false);
                        
                    }

                }
                if (Input.GetMouseButtonDown(1))
                {
                    GameView.ProtectorPanel.SetActive(false);
                    Cursor.visible = true;
                    dragging = false;
                    Destroy(Building);
                }
            }
        }
        
    }

    

    //To be called if Barracks Button is pressed 
    //All panels will closed
    //Barracks building will be instantiated
    //Set dragging true
    public void BarracksButton()
    {
        GameView.ProtectorPanel.SetActive(true);//To avoid clicking another button while in this condition
        GameView.BarracksPanel.SetActive(false);
        GameView.PowerPlantPanel.SetActive(false);
        Cursor.visible = false;
        var building = barracksFactory.GetNewInstance();//Building by barracks factory
        Building = building.gameObject;
        properBuildingScript = Building.GetComponent<ProperBuildingScript>();
        
        
        dragging = true;
    }

    //To be called if Power Plant Button is pressed 
    //All panels will closed
    //Power Plant building will be instantiated
    //Set dragging true
    public void PowerPlantButton()
    {
        GameView.ProtectorPanel.SetActive(true);//To avoid clicking another button while in this condition
        GameView.BarracksPanel.SetActive(false);
        GameView.PowerPlantPanel.SetActive(false);
        Cursor.visible = false;
        var building = powerPlantFactory.GetNewInstance();//Building by pp factory
        Building = building.gameObject;
        properBuildingScript = Building.GetComponent<ProperBuildingScript>();
        
        
        dragging = true;
    }



    //To be called if Soldier Button is pressed 
    //Cooldown panel will be enabled ans coroutine SpawnCoolDown() started
    //The a* target of the soldier set to the spawn point of the barracks that it spawned from
    //The click position object is instantiated and added to the list of click points
    //The move point anchor object is instantiated and added to the MoveToPosAnchorPoints list
    //Set cooldownBool to false 
    public void SoldierButton()
    {
        if (CooldownBool)
        {
            GameView.CooldownPanel.SetActive(true);
            StartCoroutine(SpawnCoolDown());
            
            Soldier = Instantiate(SoldierPrefab, BuildingPosition + new Vector2(0,-3f), Quaternion.identity);
            Soldier.GetComponent<SoldierScript>().soldierSpawnPointTransform = Building.GetComponent<ProperBuildingScript>().spawnPoint;
            ClickPositionOBJ = Instantiate(ClickPosition, Soldier.transform.position, Quaternion.identity);
            MoveToPosAnchorOBJ = Instantiate(MoveToPosAnchor,Building.GetComponent<ProperBuildingScript>().spawnPoint.transform.position,Quaternion.identity);
            Soldiers.Add(Soldier);
            ClickPositions.Add(ClickPositionOBJ);
            MoveToPosAnchorPoints.Add(MoveToPosAnchorOBJ);
            CooldownBool = false;
            
        }
        
    } 

    

    
    // Coroutine waits for the fill amount of cooldown image to be 0, then set cooldown bool true and the fill amount to 1 to use it again 
    IEnumerator SpawnCoolDown()
    {
        
        yield return new WaitUntil(() => CooldownImage.fillAmount == 0);
        CooldownImage.fillAmount = 1;
        CooldownBool = true;
    }

    




    


}
