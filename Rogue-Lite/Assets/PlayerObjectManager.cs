using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Photon.Pun.PunRPC]
    public void DisconnectPlayer()
    {
        FindObjectOfType<GameManager>().DisconnectPlayer();
    }
}
