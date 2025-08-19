using UnityEngine;

public class DisableOnPuzzleSolve : MonoBehaviour {
    [SerializeField] private GameObject objectToDisable;
    private void OnEnable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += Disable;
    }

    private void OnDisable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved -= Disable;
    }

    private void Disable() {
        objectToDisable.SetActive(false);
    }
}