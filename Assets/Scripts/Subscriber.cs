using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Subscriber objects

public class Subscriber : MonoBehaviour
{
    void OnEnable()
    {
        Publisher.Publish += Destroy;
    }
    
    void OnDisable()
    {
        Publisher.Publish += Destroy;
    }
    
    void Destroy()
    {
        Destroy(transform.gameObject);
    }
}
