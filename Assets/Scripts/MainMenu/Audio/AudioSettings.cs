using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

       
        AplicarMusicVolume(musicVolume);
        AplicarSFXVolume(sfxVolume);
        
        if (musicSlider != null) musicSlider.value = musicVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;
    }

    public void SetMusicVolume(float volume)
    {
        AplicarMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        AplicarSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void AplicarMusicVolume(float volume)
    {
        if (volume < 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    private void AplicarSFXVolume(float volume)
    {
        if (volume < 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}