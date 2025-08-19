using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleData
{
    public string name;
    public List<OnPuzzleSolved> objs;
    public int requiredBoxes = 2;
    [HideInInspector] public int placedBoxes = 0;
    [HideInInspector] public bool completed = false;
}

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle List")]
    public List<PuzzleData> puzzles = new();

    public void PlaceBoxInPuzzle(int puzzleIndex)
    {
        if (puzzleIndex < 0 || puzzleIndex >= puzzles.Count) return;

        PuzzleData puzzle = puzzles[puzzleIndex];

        if (puzzle.completed) return;

        puzzle.placedBoxes++;
        
        if (puzzle.placedBoxes >= puzzle.requiredBoxes)
        {
            puzzle.completed = true;
            
            foreach (var obj in puzzle.objs) {
                obj.OnSolved();
            }
        }
    }
}