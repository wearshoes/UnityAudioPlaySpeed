using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSpeedControl : MonoBehaviour
{

    /// <summary>
    /// 目前只能处理0.5-2倍速的变速播放
    /// </summary>


    public AudioSource audio;
    public Slider pitchSlider;
    public Button playBtn;
    public Text sliderText;

    private AudioMixer mixer;


    private void Awake()
    {
        InitSlider();
        sliderText.text = "1";
        playBtn.onClick.AddListener(OnClickPlayBtn);
       

        if (audio!=null&&audio.outputAudioMixerGroup!=null)
        {
            mixer = audio.outputAudioMixerGroup.audioMixer;
        }
    }

    private void InitSlider()
    {
        pitchSlider.maxValue = 2f;
        pitchSlider.minValue = 0.5f;
        pitchSlider.value = 1f;
        pitchSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void SetMixerPitch(float value)
    {
        if (mixer == null) return;
        var pitchValue = 1f / value;
        mixer.SetFloat("MixerPitch", pitchValue);
    }

    private void OnSliderValueChanged(float value)
    {
        sliderText.text = value.ToString();
        audio.pitch = value;
        SetMixerPitch(value);

    }

    private void OnClickPlayBtn()
    {
        if (audio==null)return;
        audio.Play();
    }


}
