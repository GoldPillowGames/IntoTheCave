using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelmetController : MonoBehaviour
{
    [SerializeField] private GameObject[] Helmet;
    [SerializeField] private GameObject eyes;
    [SerializeField] private Material[] materials;
    [SerializeField] private AwesomeToon.AwesomeToonHelper mesh;

    private void Start()
    {
        EquipHelmet(0);
        EquipSkin(0);
    }

    public void EquipHelmet(int index)
    {
        for(int i = 0; i < Helmet.Length; i++)
        {
            if(index == i)
            {
                Helmet[i].SetActive(true);
            }
            else
            {
                Helmet[i].SetActive(false);
            }
        }

        if(index == 0)
        {
            
            eyes.SetActive(true);
        }
        else
        {
            eyes.SetActive(false);
        }
    }

    public void EquipSkin(int index)
    {
        mesh.SetMaterial(0, materials[index]);
        mesh.Init();
    }
}
