using System;
using System.Collections.Generic;
using StatusEffects;
using UnityEngine;

public class Lava : MonoBehaviour {
    private List<StatusEffectsHandler> collidingObjectsStatus = new();
    private List<Rigidbody> rigidbodies = new();
    private GameObject player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == GameManager.Player) {
            player = other.gameObject;
            player.GetComponent<PlayerHealth>().Die();
        }
        if (other.gameObject.TryGetComponent(out StatusEffectsHandler status)) {
            collidingObjectsStatus.Add(status);
            status.AddEffect(StatusEffectsType.BURN, 999999, false);
        }
        if (other.gameObject.TryGetComponent(out Rigidbody rb)) {
            rigidbodies.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player)
            player = null;
        collidingObjectsStatus.Remove(other.gameObject.GetComponent<StatusEffectsHandler>());
        rigidbodies.Remove(other.GetComponent<Rigidbody>());
    }

    private void FixedUpdate() {
        foreach (var status in collidingObjectsStatus) {
            status.AddEffect(StatusEffectsType.BURN, 999999, false);
        }

        foreach (var rb in rigidbodies) {
            var lavaBehaviour = rb.gameObject.GetComponent<LavaBehaviour>();
            float force;
            if (lavaBehaviour is null)
                return;
            rb.velocity += Vector3.up * lavaBehaviour.forceIntensity;
            rb.velocity *= lavaBehaviour.forceReduction;
            rb.angularVelocity *= lavaBehaviour.forceReduction;
        }
    }
    
}
