using UnityEngine;

namespace DefaultNamespace {
    public class VenomHandler : MonoBehaviour{
        public Material mat;
        private bool poisoned;
        public float minValue ;
        public float maxValue;
        private float currentValue;
        public float rateOfChange;
        private void Update() {
            currentValue = mat.GetFloat("_VignetteIntensity");
            Debug.Log($"Current value: {currentValue}");
            if (poisoned) {
                currentValue = Mathf.Min(maxValue, currentValue + rateOfChange);
                Debug.Log($"Added. Current Value: {currentValue}");
            }
            else {
                currentValue = Mathf.Max(minValue, currentValue - rateOfChange);
                Debug.Log($"Subtracted. Current Value: {currentValue}");
            }
            mat.SetFloat("_VignetteIntensity", currentValue);
        }

        public void SetPoisoned(bool poisoned) {
            this.poisoned  = poisoned;
        }
    }
}