using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DamageText
{
    public static void Spawn(int damage, Vector3 location)
    {
        GameObject text = GameObject.Instantiate(Resources.Load<GameObject>(Path.Combine("PhotonPrefabs", "DamageText")), location, Quaternion.identity);
        text.GetComponent<DamageTextController>().Init(damage);
    }
}
