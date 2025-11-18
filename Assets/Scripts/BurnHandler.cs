using UnityEngine;

namespace DefaultNamespace {
    public class BurnHandler : MonoBehaviour{
        public Material mat;
        private bool burning;
        public float minValue ;
        public float maxValue;
        private float currentValue;
        public float rateOfChange;
        private void Update() {
            currentValue = mat.GetFloat("_VignetteIntensity");
            if (burning) {
                Debug.Log("Burning");
                currentValue = Mathf.Min(maxValue, currentValue + rateOfChange);
            }
            else {
                currentValue = Mathf.Max(minValue, currentValue - rateOfChange);
            }
            mat.SetFloat("_VignetteIntensity", currentValue);
        }

        public void SetBurning(bool burning) {
            this.burning  = burning;
        }
    }
}