using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollScript : MonoBehaviour
{
    
    public GameObject ContentPanel1;
    public GameObject ContentPanel2;
    public GameObject ContentPanel3;
    public float speed = 20f;// speed of scrolling
    private Vector3 pos1,pos2,pos3;// Positions of Content Panels
    
    private bool LoopBool1 = true; // Bool used for transforming ContentPanel1
    private bool LoopBool2 = false;// Bool used for transforming ContentPanel2
    private bool LoopBool3 = false;// Bool used for transforming ContentPanel3
    public bool IsMouseOverUI;
    void Start()
    {
        pos1 = ContentPanel1.transform.localPosition; ///
        pos2 = ContentPanel2.transform.localPosition; // first positions of content panels are set
        pos3 = ContentPanel3.transform.localPosition; ///
    }

    
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())// If mouse is over contents
        {
            

            ScrollDownLooping();
            ScrollUpLooping();
            ScrollMouseMovement();
            IsMouseOverUI = true;
        }
        else
        {
            IsMouseOverUI = false;
        }

        



    }


    // If the y axis of a content panel is more than 600 translate it to down
    void ScrollUpLooping()
    {
        if (ContentPanel2.GetComponent<RectTransform>().localPosition.y >= 600 && LoopBool1)
        {
            Vector3 v = new Vector3(ContentPanel2.GetComponent<RectTransform>().localPosition.x, ContentPanel2.GetComponent<RectTransform>().localPosition.y - 1080, 0);
            pos2 = v;
            ContentPanel2.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = false;
            LoopBool2 = false;
            LoopBool3 = true;
        }
        if (ContentPanel3.GetComponent<RectTransform>().localPosition.y >= 600 && LoopBool2)
        {
            Vector3 v = new Vector3(ContentPanel3.GetComponent<RectTransform>().localPosition.x, ContentPanel3.GetComponent<RectTransform>().localPosition.y - 1080, 0);
            pos3 = v;
            ContentPanel3.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = true;
            LoopBool2 = false;
            LoopBool3 = false;
        }
        if (ContentPanel1.GetComponent<RectTransform>().localPosition.y >= 600 && LoopBool3)
        {
            Vector3 v = new Vector3(ContentPanel1.GetComponent<RectTransform>().localPosition.x, ContentPanel1.GetComponent<RectTransform>().localPosition.y - 1080, 0);
            pos1 = v;
            ContentPanel1.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = false;
            LoopBool2 = true;
            LoopBool3 = false;
        }
    }


    // If the y axis of a content panel is less than -600 translate it to up 
    void ScrollDownLooping()
    {
        if (ContentPanel3.GetComponent<RectTransform>().localPosition.y <= -600 && LoopBool1)
        {
            Vector3 v = new Vector3(ContentPanel3.GetComponent<RectTransform>().localPosition.x, ContentPanel3.GetComponent<RectTransform>().localPosition.y + 1080, 0);
            pos3 = v;
            ContentPanel3.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = false;
            LoopBool2 = true;
            LoopBool3 = false;
        }
        if (ContentPanel1.GetComponent<RectTransform>().localPosition.y <= -600 && LoopBool2)
        {
            Vector3 v = new Vector3(ContentPanel1.GetComponent<RectTransform>().localPosition.x, ContentPanel1.GetComponent<RectTransform>().localPosition.y + 1080, 0);
            pos1 = v;
            ContentPanel1.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = false;
            LoopBool2 = false;
            LoopBool3 = true;
        }
        if (ContentPanel2.GetComponent<RectTransform>().localPosition.y <= -600 && LoopBool3)
        {
            Vector3 v = new Vector3(ContentPanel2.GetComponent<RectTransform>().localPosition.x, ContentPanel2.GetComponent<RectTransform>().localPosition.y + 1080, 0);
            pos2 = v;
            ContentPanel2.GetComponent<RectTransform>().localPosition = v;
            LoopBool1 = true;
            LoopBool2 = false;
            LoopBool3 = false;
        }
    }


    // Scroll interactions that make panels go up and down synchronously
    void ScrollMouseMovement()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            
            pos1.y += speed;
            pos2.y += speed;
            pos3.y += speed;
            ContentPanel1.GetComponent<RectTransform>().localPosition = pos1;
            ContentPanel2.GetComponent<RectTransform>().localPosition = pos2;
            ContentPanel3.GetComponent<RectTransform>().localPosition = pos3;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            
            pos1.y -= speed;
            pos2.y -= speed;
            pos3.y -= speed;
            ContentPanel1.GetComponent<RectTransform>().localPosition = pos1;
            ContentPanel2.GetComponent<RectTransform>().localPosition = pos2;
            ContentPanel3.GetComponent<RectTransform>().localPosition = pos3;
        }
    }


    
}
