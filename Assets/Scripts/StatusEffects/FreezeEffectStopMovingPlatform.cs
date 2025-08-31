using UnityEngine;

namespace StatusEffects {
    [RequireComponent(typeof(MovingPlatform))]
    public class FreezeEffectStopMovingPlatform : MonoBehaviour, IEffectBehaviour {
        private MovingPlatform movingPlatformComponent;
        private float speed;
        public StatusEffectsType GetType() {
            return StatusEffectsType.FREEZE;
        }

        public void Enable() {
            Debug.Log("Enable" + StatusEffectsType.FREEZE);
            movingPlatformComponent = GetComponent<MovingPlatform>();
            speed = movingPlatformComponent.speed;

            LeanTween.value(gameObject, speed, 0, 1).setOnUpdate((float val) => {
                movingPlatformComponent.speed = val;
            });
        }

        public void Disable() {
            Debug.Log("Disable" + StatusEffectsType.FREEZE);

            LeanTween.value(gameObject, 0, speed, 1).setOnUpdate((float val) => {
                movingPlatformComponent.speed = val;
            });
        }

        public void Tick() {
        }
    }
}
