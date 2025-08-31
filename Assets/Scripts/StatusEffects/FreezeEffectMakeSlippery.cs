namespace StatusEffects {
    using UnityEngine;

namespace StatusEffects {
    [RequireComponent(typeof(Collider))]
    public class FreezeEffectMakeSlippery : MonoBehaviour, IEffectBehaviour {
        private Collider collider;
        private PhysicMaterial originalMaterial;
        [SerializeField] private PhysicMaterial newPhysicsMaterial;
        
        public StatusEffectsType GetType() {
            return StatusEffectsType.FREEZE;
        }

        public void Enable() {
            Debug.Log("Enable" + StatusEffectsType.FREEZE);
            if (collider == null) {
                collider = GetComponent<Collider>();
                originalMaterial = collider.material;
            }

            if (collider.material == originalMaterial) {
                collider.material = newPhysicsMaterial;
            }
        }

        public void Disable() {
            Debug.Log("Disable" + StatusEffectsType.FREEZE);

            collider.material = originalMaterial;
        }

        public void Tick() {
        }
    }
}

}