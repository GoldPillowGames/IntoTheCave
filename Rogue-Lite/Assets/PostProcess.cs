using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class PostProcess
{
    public static Volume volume => GameObject.FindObjectOfType<Volume>();

    public static void SetGammaIntensity(float value)
    {
        LiftGammaGain tmp;
        volume.profile.TryGet<LiftGammaGain>(out tmp);
        tmp.gamma.value = new Vector4(tmp.gamma.value.x, tmp.gamma.value.y, tmp.gamma.value.z, value);
    }

    public static void SetGainIntensity(float value)
    {
        LiftGammaGain tmp;
        volume.profile.TryGet<LiftGammaGain>(out tmp);
        tmp.gain.value = new Vector4(tmp.gamma.value.x, tmp.gamma.value.y, tmp.gamma.value.z, value);
    }

    public static void SetLiftIntensity(float value)
    {
        LiftGammaGain tmp;
        volume.profile.TryGet<LiftGammaGain>(out tmp);
        tmp.lift.value = new Vector4(tmp.gamma.value.x, tmp.gamma.value.y, tmp.gamma.value.z, value);
    }

    public static void SetPostExposure(float value)
    {
        ColorAdjustments tmp;
        volume.profile.TryGet<ColorAdjustments>(out tmp);
        tmp.postExposure.value = value;
    }
}
