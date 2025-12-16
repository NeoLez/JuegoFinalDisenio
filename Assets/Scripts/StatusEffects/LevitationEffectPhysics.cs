using UnityEngine;
using Facts;
using System.Collections.Generic;

namespace StatusEffects {
    public class LevitationEffectPhysics : MonoBehaviour, IEffectBehaviour {
        [Header("Waypoint Configuration")]
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private bool useWaypoint = false;
        [SerializeField] private bool loopWaypoints = false;
        
        [Header("Movement Settings")]
        private Vector3 targetPosition;
        private float initialTime;
        
        [SerializeField] private float maxForce;
        [SerializeField] private float snappiness;
        [SerializeField] private float verticalOffset;
        [SerializeField] private float motionSwingAmplitude;
        
        private Rigidbody rb;
        private int currentWaypointIndex = 0;
        
        public new StatusEffectsType GetType() {
            return StatusEffectsType.LEVITATION;
        }

        public void Enable() {
            rb = GetComponent<Rigidbody>();
            initialTime = Time.time;
            
            currentWaypointIndex = 0;
            
            if (useWaypoint && waypoints.Count > 0 && waypoints[0] != null) {
                targetPosition = waypoints[0].position;
            } else {
                targetPosition = transform.position;
            }
            
            Events.ON_PLAYER_USE_DASH.Subscribe(OnDashUsed);
        }

        public void Disable() {
            Events.ON_PLAYER_USE_DASH.Unsubscribe(OnDashUsed);
        }

        public void Tick() {
            Vector3 oscillation = Vector3.up * (Mathf.Sin(Time.time - initialTime) * motionSwingAmplitude);
            Vector3 desiredPosition = targetPosition + oscillation + (Vector3.up * verticalOffset);
            Vector3 movementVector = desiredPosition - transform.position;
            rb.velocity = Vector3.Lerp(rb.velocity, movementVector, snappiness);
        }
        
        private void OnDashUsed(Unit _) {
            if (!useWaypoint || waypoints.Count == 0) return;
            
            currentWaypointIndex++;
            
            if (currentWaypointIndex >= waypoints.Count) {
                if (loopWaypoints) {
                    currentWaypointIndex = 0;
                } else {
                    currentWaypointIndex = waypoints.Count - 1;
                    return;
                }
            }
            
            if (waypoints[currentWaypointIndex] != null) {
                targetPosition = waypoints[currentWaypointIndex].position;
                Debug.Log($"{gameObject.name} waypoint {currentWaypointIndex + 1}");
            }
        }
    }
}