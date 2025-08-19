using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem {
    public static class FootstepMaterialDatabase {

        public static readonly Dictionary<MaterialType, FootstepMaterialSO> Dictionary = new();
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void LoadThings() {
            Dictionary[MaterialType.None] = null;
            Dictionary[MaterialType.Carpet] = Resources.Load<FootstepMaterialSO>("Footsteps/Carpet");
            Dictionary[MaterialType.Wood] = Resources.Load<FootstepMaterialSO>("Footsteps/Wood");
            Dictionary[MaterialType.Stone] = Resources.Load<FootstepMaterialSO>("Footsteps/Stone");
        }
    }
}