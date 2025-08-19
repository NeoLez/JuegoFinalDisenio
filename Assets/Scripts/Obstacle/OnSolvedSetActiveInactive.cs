using System.Collections.Generic;
using UnityEngine;

public class OnSolvedSetActiveInactive : OnPuzzleSolved {
    [SerializeField] private List<GameObject> objectsToSet;
    [SerializeField] private bool value;

    public override void OnSolved() {
        foreach (var obj in objectsToSet) {
            obj.SetActive(value);
        }
    }
}
        
