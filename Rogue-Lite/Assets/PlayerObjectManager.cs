using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void ShowDeathMenu()
    {
        foreach(PlayerController player in FindObjectsOfType<PlayerController>())
        {
            if (player.isMe)
            {
                FindObjectOfType<TimeManager>().timeScale = 0.0f;
                Time.timeScale = 0.0f;
                player.isDead = true;
                player.UI.ShowDeathMenu();
            }
        }
        //if(GetComponent<PhotonView>().IsMine)
        //    GetComponentInChildren<PlayerController>().UI.ShowDeathMenu();
    }

    [PunRPC]
    public void DisconnectPlayer()
    {
        FindObjectOfType<GameManager>().DisconnectPlayer();
    }
}
