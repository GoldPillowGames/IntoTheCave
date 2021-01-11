using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoldPillowGames.Enemy;

public class EmissiveMaterialController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer mesh = null;
    [SerializeField] private int[] emissiveMaterialIndexes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyController>().enabled == false)
        {
            // mesh.materials[6].color = Color.black;

            for (int i = 0; i < emissiveMaterialIndexes.Length; i++)
            {
                mesh.materials[emissiveMaterialIndexes[i]].SetColor("_EmissionColor", Color.Lerp(mesh.materials[emissiveMaterialIndexes[i]].GetColor("_EmissionColor"), Color.black, 4f * Time.deltaTime));
                mesh.GetComponent<AwesomeToon.AwesomeToonHelper>().SetMaterial(emissiveMaterialIndexes[i], mesh.materials[emissiveMaterialIndexes[i]]);
            }
            mesh.GetComponent<AwesomeToon.AwesomeToonHelper>().Init();
        }
    }
}
