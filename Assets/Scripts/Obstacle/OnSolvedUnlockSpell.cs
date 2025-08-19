using System.Collections.Generic;
using UnityEngine;

namespace Obstacle {
    public class OnSolvedUnlockSpell : OnPuzzleSolved {
        [SerializeField] private List<DrawingPatternSO> DrawingPatterns;
        public override void OnSolved() {
            foreach (var pattern in DrawingPatterns) {
                DrawingPatternDatabase.UnlockSpell(pattern);
            }
        }
    }
}