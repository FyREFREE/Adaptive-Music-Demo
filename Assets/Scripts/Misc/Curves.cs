using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Curves : MonoBehaviour
{
    public AnimationCurve[] curves;
    public AdaptiveAudioManager audio;
    public TMP_Dropdown dropdown;

    public void OnValueChanged()
    {
        setCurve(dropdown.value);
    }

    public void setCurve(int curve)
    {
        audio.curve = curves[curve];
    }
}
