using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraController))]
public class MobileDrawingSurface : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] private GameObject surface;
    [SerializeField] private float distance;
    private CameraController _cameraController;
    private bool _surfaceActive;

    private void Awake() {
        _cameraController = GetComponent<CameraController>();
        
        surface.SetActive(_surfaceActive);

        GameManager.Input.BookActions.OpenBook.started += OpenBook;
    }

    private void LateUpdate() {
        if (_surfaceActive) {
            Camera camera = Camera.main;
            surface.transform.position = camera.transform.position + camera.transform.forward * distance;
            surface.transform.forward = camera.transform.forward;
        }
    }

    private void OpenBook(InputAction.CallbackContext ctx) {
        if (_surfaceActive) {
            _surfaceActive = false;
            surface.SetActive(false);
            _cameraController.LockCamera();
            GameManager.Input.CameraMovement.Enable();
            GameManager.Input.CardUsage.Enable();
        }
        else {
            _surfaceActive = true;
            surface.SetActive(true);
            _cameraController.UnlockCamera();
            GameManager.Input.CameraMovement.Disable();
            GameManager.Input.CardUsage.Disable();
        }
    }
}