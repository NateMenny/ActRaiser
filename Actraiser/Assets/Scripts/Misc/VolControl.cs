using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    string masterVolParam = "MasterVol";

    // Start is called before the first frame update
    void Start()
    {
        if (slider)
            slider.onValueChanged.AddListener(HandleSliderChange);
        
    }

    void HandleSliderChange(float value)
    {
        if (mixer)
            mixer.SetFloat(masterVolParam, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
