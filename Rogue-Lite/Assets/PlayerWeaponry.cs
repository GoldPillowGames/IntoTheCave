﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponry : MonoBehaviour
{
    [Header("Weaponry")]
    [SerializeField] private int currentWeaponIndex = 0;
    [SerializeField] private GameObject[] weapon1;
    [SerializeField] private GameObject[] weapon2;
    [SerializeField] private GameObject[] weapon3;

    private Transform[] weaponTrails1;
    private Transform[] weaponTrails2;
    private Transform[] weaponTrails3;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private MeleeWeaponTrail trail;
    [SerializeField] private RuntimeAnimatorController[] controllers;
    private GameObject[] currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weapon1;
        weaponTrails1 = weapon1[0].GetComponentsInChildren<Transform>();
        weaponTrails2 = weapon2[0].GetComponentsInChildren<Transform>();
        weaponTrails3 = weapon3[0].GetComponentsInChildren<Transform>();
        SwapWeapon(currentWeaponIndex);
        
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
                trail._base = weaponTrails1[2];
                trail._tip = weaponTrails1[1];
                animator.SetBool("HasAttack3", true);
                //GetComponent<PlayerController>().weaponTrail = weapon1[0].GetComponent<MeleeWeaponTrail>();
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
                trail._base = weaponTrails2[2];
                trail._tip = weaponTrails2[1];
                animator.SetBool("HasAttack3", false);
                //GetComponent<PlayerController>().weaponTrail = weapon2[0].GetComponent<MeleeWeaponTrail>();
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
                trail._base = weaponTrails3[2];
                trail._tip = weaponTrails3[1];
                animator.SetBool("HasAttack3", false);
               // GetComponent<PlayerController>().weaponTrail = weapon3[0].GetComponentInChildren<MeleeWeaponTrail>();
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