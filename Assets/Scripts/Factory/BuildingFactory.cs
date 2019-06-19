using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T prefab;



    public T GetNewInstance()
    {
        return Instantiate(prefab,Camera.main.WorldToScreenPoint(Input.mousePosition),Quaternion.identity);
    }
}
