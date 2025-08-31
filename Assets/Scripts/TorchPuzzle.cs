using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TorchPuzzle : MonoBehaviour {
    [SerializeField] private List<BurnEffectPuzzleTorch> torches;
    [SerializeField] private List<OnPuzzleSolved> onSolvedActions;
    private bool _wasSolved;
    private void Update() {
        if (!_wasSolved) {
            _wasSolved = IsSolved();
            if(_wasSolved)
                onSolvedActions.ForEach(action => action.OnSolved());
        }
    }

    private bool IsSolved() {
        return torches.TrueForAll(torch => torch.IsInIntendedState());
    }
}