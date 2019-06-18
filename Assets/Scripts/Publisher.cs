using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
