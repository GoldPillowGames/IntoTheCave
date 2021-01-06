using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ItemSpawner : MonoBehaviour
{
    private void Awake()
    {
        ItemSpawner[] itemSpawners = FindObjectsOfType<ItemSpawner>();

        foreach(ItemSpawner itemSpawner in itemSpawners)
        {
            if(itemSpawner != this)
            {
                itemSpawner.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(this);
    }

    public void SpawnRandomItem()
    {
        int index = Random.Range(1, 25);
        if (!Config.data.isOnline)
        {
            GameObject item = Resources.Load<GameObject>(Path.Combine("PhotonPrefabs", "Item" + index.ToString("00")));
            print("Item Spawned: item" + index.ToString("00"));
            Instantiate(item, transform.position, Quaternion.identity);
        }
        else if(GetComponent<Photon.Pun.PhotonView>().IsMine)
        {
            print("PhotonNetwork.Instantiate");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Item" + index.ToString("00")), transform.position, Quaternion.identity);
        }
        
    }
}
