using System;
using SoundSystem;
using Timers;
using UnityEngine;

[RequireComponent(typeof(MaterialTypeComponent), typeof(Collider), typeof(Rigidbody))]
public class HitSounds : MonoBehaviour {
    private MaterialTypeComponent ownMaterialType;
    private Timer cooldownTimer = new Timer();
    [SerializeField] private float cooldownTime;

    private void Awake() {
        ownMaterialType = GetComponent<MaterialTypeComponent>();
    }

    private void OnCollisionEnter(Collision other) {
        if (cooldownTimer.IsCompleted()) {
            if (other.gameObject.TryGetComponent(out MaterialTypeComponent materialType)) {
                if (other.gameObject.TryGetComponent(out HitSounds hitSounds)) {
                    if (other.gameObject.GetInstanceID() > gameObject.GetInstanceID()) {
                        return; //Prevent double play of same sound from both objects
                    }
                }

                float volume = (other.relativeVelocity.magnitude - 4)/5;

                if (other.contactCount < 1) return;
                
                if(HitSoundsDatabase.FindAndPlay(ownMaterialType.materialType, materialType.materialType, other.contacts[0].point, volume))
                    cooldownTimer.Reset(cooldownTime);
            
            }
        }
    }
}