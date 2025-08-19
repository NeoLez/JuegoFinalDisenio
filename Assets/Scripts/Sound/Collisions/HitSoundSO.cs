using System.Collections.Generic;
using SoundSystem;
using UnityEngine;

namespace SoundSystem {
    [CreateAssetMenu(fileName = "FootstepsMaterial", menuName = "SO/Sound/HitSoundSO")]
    public class HitSoundSO : ScriptableObject {
        [SerializeField] private MaterialType materialA;
        [SerializeField] private MaterialType materialB;

        [SerializeField] private List<AudioClip> sounds;


        public bool IsSamePair(MaterialType a, MaterialType b) {
            return a == materialA && b == materialB || a == materialB && b == materialA;
        }

        public void PlayAudio(Vector3 position, float volume = 1) {
            if(sounds.Count > 0)
                GameManager.AudioSystem.PlaySoundPositional(sounds[Random.Range(0, sounds.Count-1)], position, GameManager.AudioSystem.VFX, volume);
        }
    }
}