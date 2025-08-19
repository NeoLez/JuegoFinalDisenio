using System.Reflection;
using UnityEngine;

namespace Facts {
    public class FactChecker : MonoBehaviour {
        [SerializeField] private bool display;
        [SerializeField] private string fieldName;
        
        private void Update() {
            if (display) {
                display = false;

                Debug.Log(Facts.TOTAL_JUMPS);
            } 
        }
    }
}
