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
            if (poisoned) {
                currentValue = Mathf.Min(maxValue, currentValue + rateOfChange);
            }
            else {
                currentValue = Mathf.Max(minValue, currentValue - rateOfChange);
            }
            mat.SetFloat("_VignetteIntensity", currentValue);
        }

        public void SetPoisoned(bool poisoned) {
            this.poisoned  = poisoned;
        }
    }
}