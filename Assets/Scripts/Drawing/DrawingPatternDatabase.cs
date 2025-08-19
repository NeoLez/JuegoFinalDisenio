using System.Collections.Generic;
using System.Linq;
using Optional;
using UnityEngine;

public static class DrawingPatternDatabase {
    //TP2 Leonardo Gonzalez Chiavassa
    private static readonly DrawingPatternSO[] Patterns = Resources.LoadAll<DrawingPatternSO>("Patterns");
    private static readonly HashSet<DrawingPatternSO> UnlockedPatterns = new();

    public static Option<CardInfoSO> GetSpellFromDrawing(Drawing drawing) {
        foreach (var pattern in UnlockedPatterns) {
            if (pattern.drawing.Equals(drawing)) {
                return pattern.cardInfo.SomeNotNull();
            }
        }

        return Option.None<CardInfoSO>();
    }

    public static void UnlockSpell(DrawingPatternSO drawingPattern) {
        UnlockedPatterns.Add(drawingPattern);
    }

    public static void UnlockAllSpells() {
        foreach (var pattern in Patterns) {
            UnlockedPatterns.Add(pattern);
        }
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RestLearntSpells() {
        UnlockedPatterns.Clear();
    }
}