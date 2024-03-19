using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioUIBridger : MonoBehaviour
{
    public AdaptiveAudioManager audio;
    public SliderController[] sliders;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(SliderController s in sliders)
        {
           s.soundArray = this.gameObject.GetComponent<AdaptiveAudioManager>().layers;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
