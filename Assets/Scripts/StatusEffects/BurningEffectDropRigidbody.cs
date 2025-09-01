using UnityEngine;

namespace StatusEffects {
    [RequireComponent(typeof(Health))]
    public class BurningEffectDropRigidbody : MonoBehaviour, IEffectBehaviour {
        [SerializeField] private Rigidbody rb;
        public StatusEffectsType GetType() {
            return StatusEffectsType.BURN;
        }

        public void Enable() {
            Invoke("MakeFall", 3);
        }

        private void MakeFall() {
            rb.gameObject.layer = LayerMask.NameToLayer("Ground");
            rb.constraints = RigidbodyConstraints.None;
        }

        public void Disable() {
        }

        private float tick;
        public void Tick() {
        }

        private void OnDeath() {
        }
    }
}