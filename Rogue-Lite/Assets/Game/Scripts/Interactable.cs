using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] float checkRadius = 5;

    Outline outlineComponent;

    // Start is called before the first frame update
    void Start()
    {
        outlineComponent = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, checkRadius, whatIsPlayer))
        {
            outlineComponent.currentColor.a = 1;
        }
        else
        {
            outlineComponent.currentColor.a = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

    }
}
