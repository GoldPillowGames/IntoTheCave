using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponry : MonoBehaviour
{
    [Header("Weaponry")]
    [SerializeField] private int currentWeaponIndex;
    [SerializeField] private GameObject[] weapon1;
    [SerializeField] private GameObject[] weapon2;
    [SerializeField] private GameObject[] weapon3;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController[] controllers;
    private GameObject[] currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weapon1;
    }

    void SwapWeapon(int index)
    {
        currentWeaponIndex = index;
        animator.runtimeAnimatorController = controllers[index];
        
        switch (index)
        {
            case 0:
                
                for (int i = 0; i < weapon1.Length; i++)
                {
                    weapon1[i].SetActive(true);
                }
                for (int i = 0; i < weapon2.Length; i++)
                {
                    weapon2[i].SetActive(false);
                }
                for (int i = 0; i < weapon3.Length; i++)
                {
                    weapon3[i].SetActive(false);
                }
                GetComponent<PlayerController>().weaponTrail = weapon1[0].GetComponent<MeleeWeaponTrail>();
                break;
            case 1:
                for (int i = 0; i < weapon1.Length; i++)
                {
                    weapon1[i].SetActive(false);
                }
                for (int i = 0; i < weapon2.Length; i++)
                {
                    weapon2[i].SetActive(true);
                }
                for (int i = 0; i < weapon3.Length; i++)
                {
                    weapon3[i].SetActive(false);
                }
                GetComponent<PlayerController>().weaponTrail = weapon2[0].GetComponent<MeleeWeaponTrail>();
                break;
            case 2:
                for (int i = 0; i < weapon1.Length; i++)
                {
                    weapon1[i].SetActive(false);
                }
                for (int i = 0; i < weapon2.Length; i++)
                {
                    weapon2[i].SetActive(false);
                }
                for (int i = 0; i < weapon3.Length; i++)
                {
                    weapon3[i].SetActive(true);
                }
                GetComponent<PlayerController>().weaponTrail = weapon3[0].GetComponentInChildren<MeleeWeaponTrail>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(2);
        }
    }
}
