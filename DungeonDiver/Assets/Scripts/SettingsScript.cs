using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private void Start()
    {
        float tmp;
        audioMixer.GetFloat("MasterVolume", out tmp);
        volumeSlider.value = tmp;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
