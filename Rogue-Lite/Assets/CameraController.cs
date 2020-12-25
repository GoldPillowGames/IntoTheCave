using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraState
{
    IDLE,
    INTERACT,
    DIALOGUE,
    END_ROOM
}

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cmCamera;
    CinemachineFramingTransposer cmTransposer;

    public CameraState cameraState;

    public float idleDistance = 42;
    public float interactDistance;
    public float endRoomDistance = 30;

    private void Awake()
    {
        DontDestroyOnLoad(cmCamera);
    }

    // Start is called before the first frame update
    void Start()
    {
        cmTransposer = cmCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        //idleDistance = cmTransposer.m_CameraDistance;
        interactDistance = idleDistance / 1.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraState)
        {
            case CameraState.IDLE:
                //cmTransposer.m_CameraDistance = idleDistance;
                cmTransposer.m_CameraDistance = Mathf.Lerp(cmTransposer.m_CameraDistance, idleDistance, 3 * Time.deltaTime);
                break;
            case CameraState.INTERACT:
                cmTransposer.m_CameraDistance = Mathf.Lerp(cmTransposer.m_CameraDistance, interactDistance, 4 * Time.deltaTime);
                break;
            case CameraState.DIALOGUE:
                break;
            case CameraState.END_ROOM:
                cmTransposer.m_CameraDistance = Mathf.Lerp(cmTransposer.m_CameraDistance, endRoomDistance, 4 * Time.deltaTime);
                break;
        }
    }
}
