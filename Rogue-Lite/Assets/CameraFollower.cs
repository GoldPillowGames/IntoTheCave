using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject playerReference;
    public int playerIndex = 0;

    private bool moveToPlayer = false;

    private void Update()
    {
        if(moveToPlayer)
            transform.position = Vector3.Lerp(transform.position, playerReference.transform.position, 100*Time.unscaledDeltaTime);
    }

    public void StopFollowing()
    {
        transform.parent = null;
    }

    public void ResetFollowing()
    {
        //moveToPlayer = true;
        //moveToPlayer = false;
        transform.parent = playerReference.transform;
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
