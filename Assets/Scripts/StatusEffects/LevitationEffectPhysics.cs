using UnityEngine;

namespace StatusEffects {
    public class LevitationEffectPhysics : MonoBehaviour, IEffectBehaviour {
        private Vector3 initialPosition;
        private float initialTime;
        [SerializeField] private float maxForce;

        [SerializeField] private float snappiness;

        [SerializeField] private float verticalOffset;

        [SerializeField] private float motionSwingAmplitude;
        
        private Rigidbody rb;
        public new StatusEffectsType GetType() {
            return StatusEffectsType.LEVITATION;
        }

        public void Enable() {
            initialPosition = transform.position;
            rb = GetComponent<Rigidbody>();
            initialTime = Time.time;
        }

        public void Disable() {
        }

        public void Tick() {
            Vector3 newPos = initialPosition + Vector3.up * (Mathf.Sin(Time.time - initialTime) * motionSwingAmplitude);
            Vector3 movementVector = newPos - transform.position + verticalOffset * Vector3.up;
            //if (movementVector.magnitude > maxForce) {
             //   movementVector.Normalize();
             //   movementVector *= maxForce;
            //}
            rb.velocity = Vector3.Lerp(rb.velocity, movementVector, snappiness);
        }
    }
}
