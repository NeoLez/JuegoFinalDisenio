using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(checkpointID, transform.position);
        }
    }
}