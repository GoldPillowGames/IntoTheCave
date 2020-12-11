using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{

    Transform mainCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCameraTransform);
    }
}
