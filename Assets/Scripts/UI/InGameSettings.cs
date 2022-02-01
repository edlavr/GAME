using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour
{
    private VolumeSlider vs;
    private void Awake()
    {
        vs = VolumeSlider.Instance;
        vs.slider = GetComponent<Slider>();
        vs.text = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        var value = PlayerPrefs.GetFloat("volume");
        vs.slider.value = value;
        SliderValueChanged(value);
    }

    public void SliderValueChanged(float value)
    {
        vs.SliderValueChanged(value);
    }
}
