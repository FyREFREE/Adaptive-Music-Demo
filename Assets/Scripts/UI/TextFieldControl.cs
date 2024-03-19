using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFieldControl : MonoBehaviour
{
    public AdaptiveAudioManager a;
    private TMP_InputField me;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enter()
    {
        //Debug.Log(me.text);
        //Debug.Log(float.Parse(me.text));
        a.fadeDuration = float.Parse(me.text);
    }
}
