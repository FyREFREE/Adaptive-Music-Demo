using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// This script is for storage and public access of AnimationCurves.
// This allows you to create custom animation curves in the editor to be accessed by other scripts.
// Made for the AdaptiveAudioManager but works for anything really.

public class Curves : MonoBehaviour
{
    [Tooltip("Create and store your curves here.")] public AnimationCurve[] curves;
    [Tooltip("Setting this variable allows you to control the current curve of AAM from this script using setCurve(int)")] public AdaptiveAudioManager audio;
    [Tooltip("For UI. lets you change the current curve of AAM from a dropdown.")] public TMP_Dropdown dropdown;

    public void OnValueChanged()
    {
        setCurve(dropdown.value);
    }

    public void setCurve(int curve)
    {
        audio.defaultCurve = curves[curve];
    }
}
