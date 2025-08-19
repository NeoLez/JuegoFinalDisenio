using UnityEngine;

namespace StatusEffects {
    [RequireComponent(typeof(Health))]
    public class BurningEffectPhysicsObject : MonoBehaviour, IEffectBehaviour {
        private AudioSource source;
        [SerializeField] private AudioClip fireEffectSound;
        [SerializeField] private AudioClip fireImpactSound;
        [SerializeField] private GameObject fireParticlesPrefab;
        private GameObject fireParticles;
        private Health health;
        private bool isDead;
        
        public StatusEffectsType GetType() {
            return StatusEffectsType.BURN;
        }

        public void Enable() {
            health = GetComponent<Health>();
            health.OnDeath += OnDeath;
            
            fireParticles = Instantiate(fireParticlesPrefab);
            
            Renderer renderer = GetComponent<Renderer>();
            Material objMaterial = new Material(renderer.material);
            renderer.material = objMaterial;

            float fireAmount = 1;
            source = GameManager.AudioSystem.PlaySoundLooping(fireEffectSound, gameObject.transform.position);
            GameManager.AudioSystem.PlaySound(fireImpactSound);
            LeanTween.value(gameObject, fireAmount, -0.6f, 1).setOnUpdate((float val) =>
            {
                fireAmount = val;
                objMaterial.SetFloat("_FireTransition", fireAmount);
            });
        }

        public void Disable() {
            if (fireParticles != null) {
                Destroy(fireParticles);
                fireParticles = null;
            }
            
            Renderer renderer = GetComponent<Renderer>();
            Material objMaterial = new Material(renderer.material);
            renderer.material = objMaterial;
            float fireAmount = objMaterial.GetFloat("_FireTransition");
            LeanTween.value(gameObject, fireAmount, 1, 1).setOnUpdate((float val) =>
            {
                fireAmount = val;
                objMaterial.SetFloat("_FireTransition", fireAmount);
            });
            
        }

        private float tick;
        public void Tick() {
            if (!isDead) {
                tick -= Time.deltaTime;

                source.transform.position = transform.position;

                if (tick <= 0f)
                {
                    Debug.Log("Took DMG");
                    health.TakeDamage(1);
                    tick = 1f;
                }
            }
        }

        private void OnDeath() {
            isDead = true;
            
            gameObject.layer = LayerMask.NameToLayer("Ground");
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material objMaterial = new Material(renderer.material);
            renderer.material = objMaterial;
                
            LeanTween.value(gameObject, -0.5f, 0.5f, 2f).setOnUpdate((val) =>
            {
                objMaterial.SetFloat("_Disintegrate", val);
            }).setDestroyOnComplete(true);
            
            LeanTween.value(source.gameObject, 1.0f, 0.0f, 1.5f).setOnUpdate((float val) => {
                source.volume = val;
            }).setOnComplete(_ => Destroy(source.gameObject));
        }

        private void OnCollisionEnter(Collision other) {
            if (!gameObject.GetComponent<StatusEffectsHandler>().HasEffect(StatusEffectsType.BURN)) return;
            
            if (other.gameObject.TryGetComponent(out StatusEffectsHandler handler))
            {
                if (!handler.HasEffect(StatusEffectsType.BURN)) {
                    handler.AddEffect(StatusEffectsType.BURN, 4f);
                }
            }
        }
    }
}
