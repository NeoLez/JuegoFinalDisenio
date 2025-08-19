using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawingSurfacePuzzle))]
public class UnlockSpellOnSolvePuzzle : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] private List<DrawingPatternSO> DrawingPatterns; 
    private void OnEnable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += UnlockSpell;
    }

    private void OnDisable() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved -= UnlockSpell;
    }

    private void UnlockSpell() {
        foreach (var pattern in DrawingPatterns) {
            DrawingPatternDatabase.UnlockSpell(pattern);
        }
    }
}