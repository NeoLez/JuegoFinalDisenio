using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem {
    //TP2 Belen Mounier
    public AudioSource NonPositionAudioSource;
    private List<AudioSource> currentlyLoopingSounds = new();
    public AudioMixerGroup Music;
    public AudioMixerGroup VFX;

    public void PlaySound(AudioClip audioClip, float volume = 1) {
        NonPositionAudioSource?.PlayOneShot(audioClip, volume);
    }

    public void PlaySoundPositional(AudioClip audioClip, Vector3 position, AudioMixerGroup mixerGroup, float volume = 1) {
        GameObject source = new GameObject("PositionalAudioSource");
        source.transform.position = position;

        AudioSource audioSource = source.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1f;
        audioSource.outputAudioMixerGroup = mixerGroup;
        
        audioSource.Play();
        Object.Destroy(source, audioClip.length);
    }

    public AudioSource PlaySoundLooping(AudioClip audioClip, Vector3 position, float volume = 1) {
        GameObject go = new GameObject();
        go.transform.position = position;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();

        return audioSource;
    }
    
    public AudioSource PlaySoundLooping(AudioClip audioClip, float volume = 1) {
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();

        return audioSource;
    }
}