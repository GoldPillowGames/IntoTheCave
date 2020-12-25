using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    void Start()
    {
        Audio.ActivateTrack(1);
    }

    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            g.GetComponent<BoxCollider>().enabled = true;
        }
        Audio.DeactivateTrack(1);
    }
}
