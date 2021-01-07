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
    public float dialogueDistance;
    public float endRoomDistance = 30;

    // Shake
    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

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

        if (cmCamera != null)
            virtualCameraNoise = cmCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // cmCamera.UpdateCameraState(new Vector3(0,0,0), Time.deltaTime);
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
                cmTransposer.m_CameraDistance = Mathf.Lerp(cmTransposer.m_CameraDistance, dialogueDistance, 4 * Time.deltaTime);
                break;
            case CameraState.END_ROOM:
                cmTransposer.m_CameraDistance = Mathf.Lerp(cmTransposer.m_CameraDistance, endRoomDistance, 4 * Time.deltaTime);
                break;
        }

        // If the Cinemachine componet is not set, avoid update
        if (cmCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    public void ShakeCamera(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        ShakeDuration = shakeDuration;           // Time the Camera Shake effect will last
        ShakeAmplitude = shakeAmplitude;         // Cinemachine Noise Profile Parameter
        ShakeFrequency = shakeFrequency;         // Cinemachine Noise Profile Parameter

        ShakeElapsedTime = ShakeDuration;
    }

    public void SetFollowTarget(GameObject gameObject)
    {
        cmCamera.Follow = gameObject.transform;
    }

    public void SetFollowTarget(Transform gameObject)
    {
        cmCamera.Follow = gameObject;
    }
}
