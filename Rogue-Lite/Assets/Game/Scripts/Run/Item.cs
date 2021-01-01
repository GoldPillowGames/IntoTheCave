using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviour
{
    protected string description = "Item Description";
    protected int id;

    private bool deactivated = false;
    private float radius;
    private Transform playerToFollow;
    private SphereCollider sphereCollider;
    private Rigidbody rb;

    public virtual void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        radius = sphereCollider.radius * transform.localScale.y * 3;
        print((int)Config.data.language + 2);
        print("ID: " + id);
    }

    // Update is called once per frame
    void Update()
    {
        if (deactivated)
        {
            transform.position = Vector3.Lerp(transform.position, playerToFollow.position, 6f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), 7f * Time.deltaTime);

            if(transform.localScale.x < 0.001f || Physics.CheckSphere(transform.position, 0.5f, 1 << 10))
            {
                Destroy(this.gameObject);
            }

            return;
        }
            

        if(Physics.CheckSphere(transform.position, radius, 1 << 10) && !deactivated)
        {
            if(!Config.data.isOnline)
                PickUpItem();
            else if(GetComponent<PhotonView>().isRuntimeInstantiated)
                GetComponent<PhotonView>().RPC("PickUpItem", RpcTarget.All);
        }
    }

    [PunRPC]
    public void PickUpItem()
    {
        if(id != 0)
        {
            description = TableReader.GetString("Items/Items", 2, id - 1);
            deactivated = true;
            PlayerStatus[] players = FindObjectsOfType<PlayerStatus>();
            float auxDistance = Mathf.Infinity;
            foreach (PlayerStatus player in players)
            {
                OnPickUpItem(player);
                player.GetComponent<PlayerController>().ShowItemDescription(description);
                float currentDistance = Vector3.Magnitude(player.transform.position - transform.position);
                if (currentDistance < auxDistance)
                {
                    playerToFollow = player.transform;
                    auxDistance = currentDistance;
                }
            }

            sphereCollider.enabled = false;
            rb.isKinematic = true;
            // Destroy(this.gameObject);
        }

    }

    public virtual void OnPickUpItem(PlayerStatus player)
    {

    }
}
