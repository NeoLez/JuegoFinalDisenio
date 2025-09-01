using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StatusEffects {
    public class StatusEffectsHandler : MonoBehaviour {
        private readonly Dictionary<StatusEffectsType, StatusEffectData> _statusEffects = new();

        public void AddEffect(StatusEffectsType type, float duration, bool toggle = true) {
            if (!HasEffect(type)) {
                _statusEffects[type] = new StatusEffectData(type, duration, gameObject);
            } else { 
                _statusEffects[type].ResetDuration(duration);
                if(toggle)
                    _statusEffects[type].Expire();
            }
        }

        public bool HasEffect(StatusEffectsType type) {
            return _statusEffects.ContainsKey(type);
        }

        private void Update() {
            if(_statusEffects.Keys.Count == 0) return;

            for(int i = _statusEffects.Count - 1; i >= 0; i--) {
                var key = _statusEffects.Keys.ToArray()[i];
                StatusEffectData data = _statusEffects[key];

                if (data.ExpirationTime <= Time.time) {
                    data.Expire();
                    _statusEffects.Remove(key);
                }
                else {
                    data.UpdateBehaviours();
                }
            }
        }

        private class StatusEffectData {
            public float ExpirationTime { get; private set; }
            private readonly List<IEffectBehaviour> _effectBehaviours = new ();

            public StatusEffectData(StatusEffectsType type, float duration, GameObject gameObject) {
                ExpirationTime = Time.time + duration;
                
                foreach (var behaviour in gameObject.GetComponents<IEffectBehaviour>())
                {
                    if(behaviour.GetType() == type) {
                        _effectBehaviours.Add(behaviour);
                        behaviour.Enable();
                    }
                }
            }

            public void UpdateBehaviours() 
            {
                foreach (var behviour in _effectBehaviours) {
                    behviour.Tick();
                }
            }

            public void Expire() {
                ExpirationTime = float.MinValue;
                foreach (var behaviour in _effectBehaviours) {
                    behaviour.Disable();
                }
            }

            public void ResetDuration(float duration) {
                ExpirationTime = Time.time + duration;
            }
        }
    }
}