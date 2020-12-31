using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item01 : Item
{
    public override void Start()
    {
        id = 1;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        
        player.health += player.health * 5 / 100;
    }
}
