using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampController : MonoBehaviour
{
    [SerializeField] private GameObject[] camps;
    [SerializeField] private GameObject[] tutorials1;
    [SerializeField] private GameObject[] tutorials2;

    // Start is called before the first frame update
    void Start()
    {

        // Config.ResetData();
        // Progreso del juego
        camps[0].SetActive(Config.data.dungeonsStarted >= 0 ? true : false);
        camps[1].SetActive(Config.data.dungeonsStarted >= 1 ? true : false);
        camps[2].SetActive(Config.data.dungeonsStarted >= 4 ? true : false);
        camps[3].SetActive(Config.data.dungeonsStarted >= 6 ? true : false);

        foreach(GameObject g in tutorials1)
        {
            g.SetActive(Config.data.dungeonsStarted == 0 ? true : false);
        }

        foreach (GameObject g in tutorials2)
        {
            g.SetActive(Config.data.dungeonsStarted == 1 ? true : false);
        }

        print("Dungeons Started: " + Config.data.dungeonsStarted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
