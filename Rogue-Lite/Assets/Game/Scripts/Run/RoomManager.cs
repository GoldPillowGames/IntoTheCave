using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _audioManager.ActivateDynamicTrack(1);
    }

    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            g.GetComponent<BoxCollider>().enabled = true;
        }
        _audioManager.DeactivateDynamicTrack(1);
    }
}
