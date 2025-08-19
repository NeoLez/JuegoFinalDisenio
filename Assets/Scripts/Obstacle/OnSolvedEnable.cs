using UnityEngine;

public class OnSolvedEnable : OnPuzzleSolved {
    [SerializeField] private GameObject objectToEnable;


    public override void OnSolved() {
        objectToEnable.SetActive(true);
    }
}
        
