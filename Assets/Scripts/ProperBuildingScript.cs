using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperBuildingScript : MonoBehaviour
{


    public GameObject SelectedSprite;//Sprite, shows up when structure is selected 
    public bool denying = false;// Boolean value used for detecting if a structure overlaps another

    GameController GameController;
    public bool builded;//Bool used for detecting if a structure is builded 
    public Transform spawnPoint;// Transform of spawn point of barracks 
    public GameObject DenyingSprite;// Sprite, shows up when structure overlaps another
    public GameObject AllowBuildSprite;//Sprite, shows that the structure can be builded
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();//Get the GameController component of the object tagged 'GameController'
    }


    private void Update()
    {
        
        DragWithSnapping();
        BuildingNotSelectedFunc();
        DenyingSpriteSetter();
        AllowBuilding();
        
    }
    

    //Activate Denying Sprite when denying
    void DenyingSpriteSetter()
    {
        if (denying)
        {
            DenyingSprite.SetActive(true);
        }
        else { DenyingSprite.SetActive(false); }
    }

    //Activate Allow Building Sprite when not denying any other object and also not builded
    void AllowBuilding()
    {
        if (!denying && !builded)
        {
            AllowBuildSprite.SetActive(true);
        }
        else
        {
            AllowBuildSprite.SetActive(false);
        }
    }

    //Deactivate Selected Sprite if it is not the selected structure or if another structure is selected
    void BuildingNotSelectedFunc()
    {
        if (GameController.Building == this.gameObject)
        {

        }
        else
        {
            SelectedSprite.SetActive(false);
        }


        if (GameController.Selected == false)
        {
            SelectedSprite.SetActive(false);
        }
    }

    // Gets current position, rounds its axis' to the nearest integer(snapping)
    void DragWithSnapping()
    {
        

        if (tag == "PowerPlant")
        {
            
            if (!builded)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 currentPosition = transform.position;

                transform.position = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y) + 0.5f);
                
            }
            else
            {
                
                transform.position = transform.position;
            }

        }
        else
        {
            if (!builded)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 currentPosition = transform.position;
                transform.position = new Vector2(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y));
            }
            else
            {
                transform.position = transform.position;
            }
        }
        
    }

    //Set selected structure as this one. Activate selected sprite.  
    private void OnMouseDown()
    {
        
        if (!GameController.dragging)
        {
            GameController.Building = gameObject;
            SelectedSprite.SetActive(true);
            GameController.Selected = true;
            if (gameObject.tag == "Barracks")//If structure is Barracks and if Power Plant Panel is enabled, then set Barracks Panel enabled, set Power Plant Panel disabled
            {
                if (GameController.PowerPlantPanel.activeSelf)
                {
                    GameController.PowerPlantPanel.SetActive(false);
                }
                GameController.BarracksPanel.SetActive(true);
                GameController.BuildingPosition = gameObject.transform.position;
                
            }
            if (gameObject.tag == "PowerPlant")//If structure is Power Plant and if Barracks Panel is enabled, then set Power Plant Panel enabled, set Barracks Panel disabled
            {
                if (GameController.BarracksPanel.activeSelf)
                {
                    GameController.BarracksPanel.SetActive(false);
                }
                GameController.PowerPlantPanel.SetActive(true);
                GameController.BuildingPosition = gameObject.transform.position;
            }
        }
        
    }

    //Set denying true if structure overlaps another 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!builded)
        {
            denying = true;
        }
        
    }

    //Set denying true if structure overlaps another and stay this way
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!builded)
        {
            denying = true;
        }
    }
    //Set denying false if structure does not overlap another structure anymore
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!builded)
        {
            denying = false;
        }
    }




    
}
