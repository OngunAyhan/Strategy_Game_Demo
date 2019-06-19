using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Publish the event(Backspace) and the subscribers will listen to this published event.
public class Publisher : MonoBehaviour
{
    public delegate void PublishEvent();
    public static event PublishEvent Publish;

    public void PublishTheEvent()
    {
        if (Publish != null)
        {
            Publish();
        }
    }
}
