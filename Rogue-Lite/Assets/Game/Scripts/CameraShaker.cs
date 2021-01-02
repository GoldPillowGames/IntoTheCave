using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraShaker
{
    public static void Shake(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        CameraController[] cameras = GameObject.FindObjectsOfType<CameraController>();
        foreach(CameraController camera in cameras)
        {
            camera.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency);
        }
    }
}
