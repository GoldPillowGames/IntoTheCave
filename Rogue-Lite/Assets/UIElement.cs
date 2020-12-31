using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{

    Transform mainCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        if(Camera.main)
         mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main && mainCameraTransform == null)
            mainCameraTransform = Camera.main.transform;
        else{
            transform.LookAt(mainCameraTransform);
        }
        
    }
}
