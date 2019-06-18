using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Camera movement with w,a,s,d and Mouse actions
    public float Border = 10f;
    public float speed = 20f;
    ScrollScript scrollScript;
    GameObject GameController;
    public float DefaultCameraSize;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        scrollScript = GameController.GetComponent<ScrollScript>();
        DefaultCameraSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        CameraMovementFunc();
        if (!scrollScript.IsMouseOverUI)//Call ZoomHandling() if cursor is over the scroll view panel
        {
            ZoomHandling();
        }

        if (Input.GetKeyDown(KeyCode.Space))//Set the default camera size by pressing space button
        {
            Camera.main.orthographicSize = DefaultCameraSize;
        }

    }

    
    void CameraMovementFunc()
    {
        Vector3 pos = transform.position;
        if (Input.mousePosition.y >= Screen.height - Border || Input.GetKey("w"))//If y axis of mouse position is bigger than the difference of the height of the screen and border value than y axis of the camera will be increased
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= Border || Input.GetKey("s"))//If y axis of mouse position is less than the border value than y axis of the camera will be decreased
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - Border || Input.GetKey("d"))//If x axis of mouse position is bigger than the difference of the width of the screen and border value than x axis of the camera will be increased
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= Border || Input.GetKey("a"))//If x axis of mouse position is less than the border value than x axis of the camera will be decreased 
        {
            pos.x -= speed * Time.deltaTime;                                                
        }                                                                                               //Also w,a,s,d buttons do the same functions
        transform.position = pos;    // Always set the updated transform for camera
    }

    //Zooming Function
    void ZoomHandling()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)//Zoom in
        {
            
            Camera.main.orthographicSize--;
            if (Camera.main.orthographicSize < 10)
            {
                Camera.main.orthographicSize = 10;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)//Zoom out
        {
            Camera.main.orthographicSize++;
            if (Camera.main.orthographicSize > 26)
            {
                Camera.main.orthographicSize = 26;
            }
        }
    }

}
