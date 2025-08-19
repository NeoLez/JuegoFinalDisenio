using UnityEngine;

[RequireComponent(typeof(DrawingSurfacePuzzle))]
public class OpenDoorOnPuzzleSolved : MonoBehaviour
{
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] private SystemDoor doorToOpen;

    private void Awake()
    {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += () => {
            if (doorToOpen != null)
                doorToOpen.OpenDoor();
        };
    }
}