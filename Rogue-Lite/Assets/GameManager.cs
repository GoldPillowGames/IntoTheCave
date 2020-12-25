﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
