using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item01 : Item
{

    public override void Awake()
    {
        id = 1;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.health += player.health * 5 / 100;
    }
}
