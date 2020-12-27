using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PostProcess.SetPostExposure(Config.data.brightness);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
