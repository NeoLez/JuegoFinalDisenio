using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour {
    [SerializeField] private List<Transform> platformPositions;
    [SerializeField] public float speed;
    [SerializeField] private float minimumDistance;
    private int currentTarget = 0;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 distance = platformPositions[currentTarget].position - rb.position;
        if (distance.magnitude <= minimumDistance) {
            currentTarget = (currentTarget + 1) % platformPositions.Count;
            distance = platformPositions[currentTarget].position - rb.position;
        }
        if(distance.magnitude > speed)
            distance.Normalize();
        
        rb.velocity = distance * speed;
        
        if (PlayerMovementControllerTest != null)
            PlayerMovementControllerTest.SetGroundSpeed(rb.velocity);
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