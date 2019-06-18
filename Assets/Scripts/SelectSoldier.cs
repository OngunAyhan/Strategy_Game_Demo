using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSoldier : MonoBehaviour
{
    public GameObject SelectedSoldier;// Selected soldier object
    public List<GameObject> ClickPositions = new List<GameObject>();  //List of the click positions of the soldiers


   
    //To be called when clicked on Soldier
    private void OnMouseClickToSoldier()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Get Mouse position as vector3
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);              //Convert it to vector2

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);        //Send a ray to the converted vector2 position
            
            if (hit.collider != null && hit.collider.tag == "Soldier")             //Set the ray hit object as selected soldier
            {
                

                
                SelectedSoldier = hit.collider.gameObject;
                
                
            }
            
        }


    }


    

    
    void Update()
    {
        
        OnMouseClickToSoldier();
        

        
    }
}
