using System.Collections;
using UnityEngine;
using Pathfinding;
public class SoldierScript : MonoBehaviour
{
    
    public AIPath aIPath;
    ProperBuildingScript properBuildingScript;
    public AIDestinationSetter aIDestination;
    
    public Transform soldierSpawnPointTransform;//Transform set by GameController, indicates the location where soldier will go after it spawns


    
    public GameObject MoveAwayObject;//Object that must go away from another when they collide
    public bool collided;//Bool used for detecting collusion
    public Vector2 nextPos;//Vector2 that indicates the position of the object when that object collided with another
    public GameObject newPosToGO;//Object with position of 'nextPos'. Used for setting the A* target as this object's transform
    float x,y;//Randomized variables that will be added to the current position of soldier
    
    Vector2 mousePos;//Current mouse position
    
    GameController GameController;
    SelectSoldier SelectSoldier;
    public bool OpenField = true;//Boolean used for detecting if soldier is not colliding with another
    public bool SoldierMoving;//Boolean used for detecting if soldier is moving
    public GameObject ClickedPositionOBJ;
    public GameObject MoveToPosAnchorOBJ;
    
    public GameObject SelectedSprite;//(Yellow)Selected image, indicating that the soldier is selected
    
    void Start()
    {
        //SoldierMoving = true;
        aIPath = GetComponent<AIPath>();
        aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = soldierSpawnPointTransform;
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SelectSoldier = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectSoldier>();
        SetClickPositionAndAnchor();//Set click position and moving anchor for this soldier when it spawns
        
    }

    

    private void OnMouseDown()//To be called when clicked to the soldier
    {
        if (GameController.Selected)//if Soldier is selected
        {
            GameController.Selected = false;//Set it to not selected
            GameController.PowerPlantPanel.SetActive(false);//
            GameController.BarracksPanel.SetActive(false);  //Disable Panels
        }
    }



    //Set the mouse click position and MoveToPosAnchor of the soldier from the list of click points and MoveToPosAnchorPoints
    void SetClickPositionAndAnchor()
    {
        
        for (int i = 0; i< GameController.Soldiers.Count;i++)//For all the soldiers in the scene;
        {
            if (this.gameObject == GameController.Soldiers[i])//if i is this soldier
            {
                ClickedPositionOBJ = SelectSoldier.ClickPositions[i];//Than i of ClickPositions is set to this soldier's clickPositionOBJ
                MoveToPosAnchorOBJ = GameController.MoveToPosAnchorPoints[i];//Than i of MoveToPosAnchorPoints is set to this soldier's MoveToPosAnchorOBJ
            }
        }
        
    }

    
  

    void SetMoveAwayValues()
    {
        x = Random.Range(-5f, 6f);//
        y = Random.Range(-1f, 2f);//Setting the random values of variables x and y(2.1)
        ResetTarget();//Call ResetTarget function to advance to the algorithm path 3(2.2) 
        OpenField = true;//Set OpenField boolean to true to end the condition and detect that soldier is not in an open field
    }

    void MovementDetection()//If it moves SoldierMoving is set to true, else it is false
    {
        
        if (aIPath.velocity == new Vector3(0f,0f,0f))
        {
            SoldierMoving = false;
            
        }
        else
        {
            
            SoldierMoving = true;
        }

        
        
    }

    void Update()
    {
        //Algorithm Path(2)
        if (!OpenField && !SoldierMoving)//OpenField is set by OnTrigerStay2D method to false and SoldierMoving is also false since soldier is staying(2)
        {
            SetMoveAwayValues();

        }

        MovementDetection();



        SoldierMovePosition();

        SelectedSpriteSetter();

        if (GameController.Selected)//If a structure is selected, hide the move position anchor
        {
            MoveToPosAnchorOBJ.SetActive(false);
        }
        

    }


    void SelectedSpriteSetter()//Set (Yellow)Selected image enabled and disabled
    {
        if (!GameController.Selected)
        {
            if (SelectSoldier.SelectedSoldier == this.gameObject)//If selected soldier is this one than set image enabled and show the anchor 
            {
                SelectedSprite.SetActive(true);
                if (SoldierMoving)
                {
                     
                    MoveToPosAnchorOBJ.SetActive(true);
                }
                else
                {
                    MoveToPosAnchorOBJ.SetActive(false);
                }

                
            }
            else
            {
                SelectedSprite.SetActive(false);              //If not than hide them
                MoveToPosAnchorOBJ.SetActive(false);
            }
        }
        else
        {
            SelectedSprite.SetActive(false);// If not selected disable selected image
        }


    }
    
    //Soldier right click move function
    void SoldierMovePosition()
    {
        if (SelectSoldier.SelectedSoldier == this.gameObject)// If this soldier is selected than wait for the right click 
        {
            if (Input.GetMouseButtonDown(1))//When right click is pressed
            {
                

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Get position that player clicked(mousePos)  
                ClickedPositionOBJ.transform.position = new Vector2(mousePos.x, mousePos.y);//Set the position of ClickedPositionOBJ to the mousePos
                
                
                MoveToPosAnchorOBJ.transform.position = ClickedPositionOBJ.transform.position;//Set anchor position to the clicked position
                aIDestination.target = ClickedPositionOBJ.transform;//Set A* target of the soldier to the clicked position
                
                
            }
        }
    }


    //Algorithm Path(4)
    private IEnumerator MoveAway()
    {
        int randomTime = Random.Range(1,3);//Set a random time integer between 1 and 2(4)
        yield return new WaitForSeconds(0.1f);//Until random time ends, wait for it to move to it's new position(4.1)
        
        
        aIDestination.target = null;//Set A* target as null(4.2)
        
    }

    



    //Algorithm Path(3)
    void ResetTarget()
    {
        nextPos = new Vector2(Mathf.RoundToInt(transform.position.x + x) - 0.5f, Mathf.RoundToInt(transform.position.y - y) - 0.5f);//Add randomized values of x and y to the MoveAwayObject's axis' and set this position to the newPos(3) 
        newPosToGO.transform.position = nextPos;//Set newPos to the newPosToGO object's position(3.1)
        aIDestination.target = newPosToGO.transform;//Set this soldier's A* target to the newPosToGO's transform(3.2)
        
        StartCoroutine(MoveAway());//Start coroutine MoveAway and adwance to the last part of the algorithm(3.3)
    }

    //Algorithm Path(1)
    private void OnTriggerStay2D(Collider2D collision)//Soldier enters and stays in another object's collider(1)
    {

        if (collision.tag == "Soldier")// If the tag of that colider is Soldier(1.1)
        {
            if (!SoldierMoving)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                Invoke("EnableCollider", 1f);
                OpenField = false;//Set OpenField bool to false / There is a collusion(1.2)
            }
            

        }

    }
    

    

    void EnableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
