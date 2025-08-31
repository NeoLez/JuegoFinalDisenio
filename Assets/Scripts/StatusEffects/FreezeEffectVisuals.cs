using UnityEngine;

namespace StatusEffects {
    public class FreezeEffectsVisuals : MonoBehaviour, IEffectBehaviour {
        [SerializeField] private AudioClip iceEffectSound;
        [SerializeField] private AudioClip iceEffectImpactSound;
        [SerializeField] private GameObject iceParticlesPrefab;
        private GameObject iceParticles;
        public StatusEffectsType GetType() {
            return StatusEffectsType.FREEZE;
        }

        public void Enable() {
            Debug.Log("Enable" + StatusEffectsType.FREEZE);
            
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
        }

        public void Tick() {
        }
    }
}
