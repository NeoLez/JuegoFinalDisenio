using UnityEngine;
using UnityEngine.Audio;

public class ManagerAudioSourceRegistration : MonoBehaviour {
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup vfxMixer;
    private void Awake() {
        GameManager.AudioSystem.NonPositionAudioSource = GetComponent<AudioSource>();

        GameManager.AudioSystem.VFX = vfxMixer;
        GameManager.AudioSystem.Music = musicMixer;
    }
}