using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    [SerializeField] public Vector3 rotationOverTime;
    void Update() {
        transform.rotation *= Quaternion.Euler(rotationOverTime);
    }
}
