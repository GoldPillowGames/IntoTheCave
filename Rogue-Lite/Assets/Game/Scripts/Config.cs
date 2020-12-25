using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public bool tactile = true;

    [SerializeField] private GameObject _joystick;

    private void Start()
    {
        if (!tactile)
        {
            _joystick.SetActive(false);
        }
    }
}
