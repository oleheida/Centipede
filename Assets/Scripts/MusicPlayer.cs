using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton() 
    {
        if(FindObjectsOfType(GetType()).Length > 1) //checking if there are more then 1 music player
        {
            Destroy(gameObject); //if yes, destroy the new one
        }
        else
        {
            DontDestroyOnLoad(gameObject); //making the current music player persist from scene to scene
        }
    }
}
