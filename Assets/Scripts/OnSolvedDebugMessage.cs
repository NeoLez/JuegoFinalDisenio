using UnityEngine;

public class OnSolvedDebugMessage : OnPuzzleSolved {
    [SerializeField] private string message = "Default Debug Message";
    public override void OnSolved() {
        Debug.Log(message);
    }
}