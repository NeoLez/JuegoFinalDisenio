using UnityEngine;

public class PlaySoundOnPuzzleSolve : MonoBehaviour {
    [SerializeField] private AudioClip sound;
    private void OnEnable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += Disable;
    }

    private void OnDisable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved -= Disable;
    }

    private void Disable() {
        GameManager.AudioSystem.PlaySound(sound);
    } 
}