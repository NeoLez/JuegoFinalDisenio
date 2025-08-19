using Facts;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public Transform respawnPosition;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die() {
        Events.ON_PLAYER_DIE.Raise(Unit.Default);
        _currentHealth = maxHealth;
        Respawn();
    }
    
    public void Respawn()
    {
        Vector3 checkpointPos = CheckpointManager.Instance.GetCheckpoint();

        if (checkpointPos != Vector3.zero)
        {
            transform.position = checkpointPos;
        }
    }
}
