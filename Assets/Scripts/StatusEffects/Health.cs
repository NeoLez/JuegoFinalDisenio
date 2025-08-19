using System;
using UnityEngine;

namespace StatusEffects {
    public class Health : MonoBehaviour {
        [SerializeField] private int maxHealth;
        private int currentHealth;
        public event Action OnDeath;
        
	public void Awake() {
	    currentHealth = maxHealth;
	}

        public void TakeDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                Die();
            }
        }

        public void Die() {
            OnDeath?.Invoke();
        }
    }
}
