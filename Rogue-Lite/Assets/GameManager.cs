using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
        for(int i = 0; i < transform.childCount; i++)
        {
            DontDestroyOnLoad(transform.GetChild(i));
        }
    }

    private void Start()
    {
        
    }

}
