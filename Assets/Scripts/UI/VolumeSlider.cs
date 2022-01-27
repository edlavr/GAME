using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    [SerializeField] private AudioSource audioSource;

    public static VolumeSlider Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        
        DontDestroyOnLoad(this);
        slider.value = PlayerPrefs.GetFloat("volume", 1f);
        SliderValueChanged(slider.value);
    }

    public void SliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        ChangeValueText(value);
        audioSource.volume = value;
    }

    private void ChangeValueText(float value)
    {
        if (value == 0.0f)
        {
            text.text = "mute";
            return;
        }
        text.text = ((int) (value * 100f)).ToString(CultureInfo.CurrentCulture);
    }
}
