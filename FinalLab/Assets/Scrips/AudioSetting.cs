using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener(val => AudioManager.Instance.SetMusicVolume(val));
        sfxSlider.onValueChanged.AddListener(val => AudioManager.Instance.SetSFXVolume(val));
    }
}
