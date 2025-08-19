using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem {
    public static class HitSoundsDatabase {

        private static readonly List<HitSoundSO> SoundList = new();
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void LoadThings() {
            SoundList.AddRange(Resources.LoadAll<HitSoundSO>("HitSounds"));
        }

        public static bool FindAndPlay(MaterialType a, MaterialType b, Vector3 position, float volume = 1) {
            foreach (var Sound in SoundList) {
                if (Sound.IsSamePair(a, b)) {
                    Sound.PlayAudio(position, volume);
                    return true;
                }
            }

            return false;
        }
    }
}