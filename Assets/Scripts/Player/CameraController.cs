using System;
using SoundSystem;
using Timers;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cam;
    [SerializeField] public float sensitivity = 1;

    [SerializeField] private float sideSwayAngle;
    [SerializeField] private float swaySpeed;
    [SerializeField] private float horizontalAmount;
    [SerializeField] private float verticalAmount;
    [SerializeField] private float frequency;
    [SerializeField] private float smooth;
    private Vector2 cameraBobbingOffset;
    private float currentSideSwayAngle;

    [SerializeField] private float waitTimeForFootstepStateChange = 0.05f;

    [SerializeField] private AudioClip stepSound;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    private bool walkedPreviousFrame;
    private float startedWalk;

    private PlayerInputActions _input;
    private MovementControllerTest _movementController;

    public Timer walkCancelTimer = new Timer();
    public float stepSoundTime = 0;

    private void Start() {
        _input = GameManager.Input;
        _movementController = GameManager.Player.GetComponent<MovementControllerTest>();
        cam = GameManager.MainCamera.transform;
        
        LockCamera();
    }

    private void Update() {
        Vector2 moveDir = _input.Movement.MoveDir.ReadValue<Vector2>();

        if (moveDir.magnitude > 0 && _movementController.GetState() != CharacterState.Air) {
            if (!walkedPreviousFrame) {
                walkedPreviousFrame = true;
                startedWalk = Time.time;
                stepSoundTime = (float)Math.PI / frequency;
            }
            walkCancelTimer.Reset(waitTimeForFootstepStateChange);
            HeadBob();
        } else {
            if (walkedPreviousFrame) {
                if (walkCancelTimer.IsCompleted()) {
                    walkedPreviousFrame = false;
                    cameraBobbingOffset = Vector2.Lerp(cameraBobbingOffset, Vector2.zero, smooth * Time.deltaTime);
                }
            }
        }

        if (walkedPreviousFrame) {
            stepSoundTime -= Time.deltaTime;
            if (stepSoundTime <= 0) {
                FootstepMaterialDatabase.Dictionary[_movementController.GetMaterialType()]?.PlaySound();
                stepSoundTime += (float)Math.PI * 2 / frequency;
            }
        }

        Vector3 viewBobVector = GetHorizontalDirectionRightVector().Swizzle_x0y() * cameraBobbingOffset.x + Vector3.up * cameraBobbingOffset.y;
        cam.position = cameraPosition.position + viewBobVector;
        
        yaw += _input.CameraMovement.MouseX.ReadValue<float>() * sensitivity;
        pitch += _input.CameraMovement.MouseY.ReadValue<float>() * sensitivity;
        
        sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.15f);

        pitch = Mathf.Clamp(pitch, -89f, 89f);
        if (yaw > 360)
            yaw -= 360;
        else if (yaw < 0)
            yaw += 360;

        float target = -moveDir.x * sideSwayAngle;
        currentSideSwayAngle = (target - currentSideSwayAngle) * swaySpeed + currentSideSwayAngle;
        cam.localRotation = Quaternion.Euler(-pitch, yaw, currentSideSwayAngle);
    }

    private void HeadBob() {
        float s = Mathf.Sin((Time.time - startedWalk) * frequency + (float)Math.PI / 2);
        cameraBobbingOffset.y = Mathf.Lerp(cameraBobbingOffset.y, s * verticalAmount * 1.4f, smooth * Time.deltaTime);
        cameraBobbingOffset.x = Mathf.Lerp(cameraBobbingOffset.x, Mathf.Cos((Time.time - startedWalk) * frequency / 2 + (float)Math.PI / 2) * horizontalAmount * 1.6f, smooth * Time.deltaTime);
    }

    public void AddPitch(float n) {
        pitch += n;
    }

    public void AddYaw(float n) {
        yaw += n;
    }

    public void LockCamera() {
        Cursor.visible = false;
        _input.CameraMovement.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCamera() {
        Cursor.visible = true;
        _input.CameraMovement.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public Vector2 GetHorizontalDirectionForwardVector() {
        return new Vector2(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Cos(yaw * Mathf.Deg2Rad));
    }

    public Vector2 GetHorizontalDirectionRightVector() {
        return new Vector2(Mathf.Cos(yaw * Mathf.Deg2Rad), -Mathf.Sin(yaw * Mathf.Deg2Rad));
    }
}
