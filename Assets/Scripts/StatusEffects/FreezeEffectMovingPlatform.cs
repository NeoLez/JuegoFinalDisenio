using UnityEngine;

namespace StatusEffects {
    [RequireComponent(typeof(RotatingObstacle))]
    public class FreezeEffectMovingPlatform : MonoBehaviour, IEffectBehaviour {
        [SerializeField] private AudioClip iceEffectSound;
        [SerializeField] private AudioClip iceEffectImpactSound;
        [SerializeField] private GameObject iceParticlesPrefab;
        private GameObject iceParticles;

        private RotatingObstacle rotatingObstacleComponent;
        private float rotationSpeed;
        public StatusEffectsType GetType() {
            return StatusEffectsType.FREEZE;
        }

        public void Enable() {
            Debug.Log("Enable" + StatusEffectsType.FREEZE);
            rotatingObstacleComponent = GetComponent<RotatingObstacle>();
            rotationSpeed = rotatingObstacleComponent.rotationSpeed;
            
            iceParticles = Instantiate(iceParticlesPrefab, transform);
            
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material objMaterial = new Material(renderer.material);
            renderer.material = objMaterial;
            GameManager.AudioSystem.PlaySoundPositional(iceEffectSound, gameObject.transform.position, GameManager.AudioSystem.VFX);
            GameManager.AudioSystem.PlaySoundPositional(iceEffectImpactSound, gameObject.transform.position, GameManager.AudioSystem.VFX);

            float frostAmount = 1;
            LeanTween.value(gameObject, frostAmount, -1, 1).setOnUpdate((float val) =>
            {
                frostAmount = val;
                objMaterial.SetFloat("_IceTransition", frostAmount);
            });

            LeanTween.value(gameObject, rotationSpeed, 0, 1).setOnUpdate((float val) => {
                rotatingObstacleComponent.rotationSpeed = val;
            });
        }

        public void Disable() {
            Debug.Log("Disable" + StatusEffectsType.FREEZE);

            if (iceParticles != null) {
                Destroy(iceParticles);
                iceParticles = null;
            }
            
            Renderer renderer = GetComponent<Renderer>();
            Material objMaterial = new Material(renderer.material);
            renderer.material = objMaterial;
            GameManager.AudioSystem.PlaySoundPositional(iceEffectSound, gameObject.transform.position, GameManager.AudioSystem.VFX);
                
            float frostAmount = -1;
            LeanTween.value(gameObject, frostAmount, 1, 1).setOnUpdate((float val) =>
            {
                frostAmount = val;
                objMaterial.SetFloat("_IceTransition", frostAmount);
            });

            LeanTween.value(gameObject, 0, rotationSpeed, 1).setOnUpdate((float val) => {
                rotatingObstacleComponent.rotationSpeed = val;
            });
        }

        public void Tick() {
        }
    }
}
