using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            g.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
