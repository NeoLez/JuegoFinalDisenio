using UnityEngine;

public class CloseBarrierOnPuzzleSolve : MonoBehaviour {
    public Shield shield;
    private void OnEnable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += Disable;
    }

    private void OnDisable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved -= Disable;
    }

    private void Disable() {
        shield.OpenCloseShield();
    } 
}