using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public string name;
    public TextMeshProUGUI textObj;
    public AdaptiveAudioManager audio;
    public Slider slider;
    public float maxValue;

    public Sound[] soundArray;
    public int soundArrayIndex;

    public string text;

    void Start()
    {
       slider = GetComponent<Slider>();
       slider.maxValue = maxValue;
       soundArray = audio.layers;
       textObj.text = text;
       //slider.maxValue = maxValue;
    }

    void OnGUI()
    {
       slider.value = soundArray[soundArrayIndex].source.volume;
    }
}
