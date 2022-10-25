using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] Slider sliderUI;
    private Text textSliderValue;
    Vector3 offset=new Vector3(0,0,20);

    void Start()
    {
        textSliderValue = GetComponent<Text>();

        ShowSliderIntValue();
    }
    private void Update()
    {


        textSliderValue.transform.localPosition = sliderUI.handleRect.localPosition ;
        textSliderValue.transform.position+=new Vector3(0,0,20);
    }

    public void ShowSliderValue()
    {
        if (textSliderValue != null)
            textSliderValue.text = sliderUI.value.ToString("N2");

    }
    public void ShowSliderIntValue()
    {
        if (textSliderValue != null)
            textSliderValue.text = ((int)sliderUI.value).ToString();

    }
}