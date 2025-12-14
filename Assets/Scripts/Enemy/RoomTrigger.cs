using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private SpellBlockerEnemy enemy;
    [SerializeField] private bool deactivateOnExit = false; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemy != null)
            {
                enemy.Activate();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (deactivateOnExit && other.CompareTag("Player"))
        {
            if (enemy != null)
            {
                enemy.Deactivate();
            }
        }
    }
}
