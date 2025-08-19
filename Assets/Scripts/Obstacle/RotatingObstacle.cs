using System;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour {
    [SerializeField] public float rotationSpeed;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (PlayerMovementControllerTest)
            GameManager.Player.GetComponent<CameraController>().AddYaw(rotationSpeed / Mathf.Deg2Rad * Time.deltaTime);
    }

    private void FixedUpdate() {
        rb.angularVelocity = Vector3.up * rotationSpeed;

        if (PlayerMovementControllerTest) {
            Vector3 r     = PlayerMovementControllerTest.transform.position - transform.position;  
            Vector3 omega = Vector3.up * rotationSpeed;  
            Vector3 v     = Vector3.Cross(omega, r);
            
            PlayerMovementControllerTest.SetGroundSpeed(v.Swizzle_x0z());
        }
    }

    private MovementControllerTest PlayerMovementControllerTest;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerMovementControllerTest = other.gameObject.GetComponent<MovementControllerTest>();
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerMovementControllerTest.SetGroundSpeed(Vector3.zero);
            PlayerMovementControllerTest = null;
        }
    }
}
