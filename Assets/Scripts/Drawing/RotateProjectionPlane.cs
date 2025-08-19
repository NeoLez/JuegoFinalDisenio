using UnityEngine;
using UnityEngine.InputSystem;

public class RotateProjectionPlane : MonoBehaviour {
    private bool isPlayerInRange;
    private bool firstRotation;

    [SerializeField] private DrawingPuzzleArea planeToRotate;
    void Update()
    {
        if (isPlayerInRange && Keyboard.current.eKey.wasPressedThisFrame && !planeToRotate.IsRotating())
        {
            
            if (firstRotation) {
                planeToRotate.rotation = new Vector3(-90,0,0);
            }
            else {
                planeToRotate.rotation = new Vector3(90,0,0);
            }

            firstRotation = !firstRotation;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
}