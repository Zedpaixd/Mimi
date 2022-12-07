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
        color.saturation.value = -100f;

    }
    // Start is called before the first frame update
    public void IncreaseSaturation(float levelSaturation, float second)
    {
        Debug.Log("LEVEL SATURATION" + levelSaturation);

        // if (color.saturation.value + levelSaturation <= 0f)
        StopAllCoroutines();
        StartCoroutine(SaturationPlus(levelSaturation, second));
    }
    public void DecreaseSaturation(float levelSaturation, float second)
    {
        Debug.Log("LEVEL SATURATION" + levelSaturation);

        // if (color.saturation.value - levelSaturation > -100f)
        StopAllCoroutines();
        StartCoroutine(SaturationLess(levelSaturation, second));
    }
    public void UpdateSaturation(float levelSaturation, float second)
    {
        Debug.Log("LEVEL SATURATION" + levelSaturation);
        if (levelSaturation >= color.saturation.value)
        {
            IncreaseSaturation(Mathf.Abs(levelSaturation), second);
        }
        else
        {
            DecreaseSaturation(Mathf.Abs(levelSaturation), second);
        }
    }

    IEnumerator SaturationPlus(float levelSaturation, float second)
    {
        float beginValue = color.saturation.value;

        while (color.saturation.value < beginValue + levelSaturation && color.saturation.value < 0f)
        {
            color.saturation.value += levelSaturation * Time.smoothDeltaTime;
            yield return new WaitForSecondsRealtime(second * Time.unscaledDeltaTime);
        }

    }
    IEnumerator SaturationLess(float levelSaturation, float second)
    {
        float beginValue = color.saturation.value;

        while (color.saturation.value > beginValue - levelSaturation)
        {
            color.saturation.value -= levelSaturation * Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(second * Time.unscaledDeltaTime);
        }

    }
}
