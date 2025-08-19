using UnityEngine;

public class ManagerPlayerRegistration : MonoBehaviour{
    private void Awake() {
        //TP2 Belen Mounier
        GameManager.Player = gameObject;
        GameManager.MainCamera = Camera.main;
        GameManager.CamFOV = GameManager.MainCamera.fieldOfView;
    }
}