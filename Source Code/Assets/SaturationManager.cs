using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.UI;



public class SaturationManager : MonoBehaviour
{
    Volume globalVolume;
    VolumeProfile profile;
    ColorAdjustments color;
    private void Start()
    {
        globalVolume = GetComponent<Volume>();
        VolumeProfile profile = globalVolume.sharedProfile;
        profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value=-100f;

    }
    // Start is called before the first frame update
    public void IncreaseSaturation(float levelSaturation, float second)
    {
        if (color.saturation.value + levelSaturation < 0f)
            StartCoroutine(SaturationPlus(levelSaturation, second));
    }
    public void DecreaseSaturation(float levelSaturation, float second)
    {
        if (color.saturation.value - levelSaturation > -100f)
            StartCoroutine(SaturationLess(levelSaturation, second));
    }

    IEnumerator SaturationPlus(float levelSaturation, float second)
    {
        float beginValue = color.saturation.value;

        while (color.saturation.value < beginValue + levelSaturation)
        {
            color.saturation.value += levelSaturation * Time.deltaTime;
            yield return new WaitForSeconds(second * Time.deltaTime);
        }

    }
    IEnumerator SaturationLess(float levelSaturation, float second)
    {
        float beginValue = color.saturation.value;

        while (color.saturation.value > beginValue - levelSaturation)
        {
            color.saturation.value -= levelSaturation * Time.deltaTime;
            yield return new WaitForSeconds(second * Time.deltaTime);
        }

    }
}
