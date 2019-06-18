
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            
            Destroy(this.gameObject);
            return;
        }
        else
        {
            
            Instance = this;
            DontDestroyOnLoad(this.gameObject);//Translating this to another scene
        }
        
    }
}
