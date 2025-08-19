using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCubePuzzle : MonoBehaviour
{
    public GameObject door;
    [SerializeField] private bool cubeInZoneA = false;
    [SerializeField] private bool cubeInZoneB = false;

    public void CubeEnter(string zone)
    {
        if (zone == "A")
        {
            cubeInZoneA = true;
        }
        if (zone == "B")
        {
            cubeInZoneB = true;
        }

        Checkpuzzle();
    }

    public void Checkpuzzle()
    {
        if (cubeInZoneA && cubeInZoneB)
        {
            door.SetActive(false);
        }
    }
}
