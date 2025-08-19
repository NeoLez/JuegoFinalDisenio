using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private int currentCheckpointID = -1;
    private Vector3 currentCheckpointPosition;
    private Quaternion currentCheckpointRotation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(int id, Vector3 position)
    {
        currentCheckpointID = id;
        currentCheckpointPosition = position;
        
    }

    public Vector3 GetCheckpoint()
    {
        return currentCheckpointPosition;
    }

    public int GetCheckpointID()
    {
        return currentCheckpointID;
    }
}