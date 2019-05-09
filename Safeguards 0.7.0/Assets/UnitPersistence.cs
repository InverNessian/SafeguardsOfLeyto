using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPersistence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewScene()
    {
        //check against the new scene, if it's not a menu scene then set obj to Don'tDestroyOnLoad
        if (1 == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {

        }
    }
}
