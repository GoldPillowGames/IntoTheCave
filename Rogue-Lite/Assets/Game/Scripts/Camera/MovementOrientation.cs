using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOrientation : MonoBehaviour
{
    [Tooltip("Main camera reference.")]
    [SerializeField] Camera cam;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
