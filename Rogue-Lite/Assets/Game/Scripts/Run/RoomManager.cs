using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            g.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
