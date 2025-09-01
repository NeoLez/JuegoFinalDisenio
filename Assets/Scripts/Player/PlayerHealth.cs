using Facts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public Transform respawnPosition;
    private int _currentHealth;

    [SerializeField] private Image panelImage;

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
        GetComponent<MovementControllerTest>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((float val) =>
        {
            panelImage.color = new Color(0, 0, 0, val);
        }).setOnComplete(o => {
            Invoke("Respawn", 1);
        });
        _currentHealth = maxHealth;
    }
    
    public void Respawn()
    {
        Vector3 checkpointPos = CheckpointManager.Instance.GetCheckpoint();

        if (checkpointPos != Vector3.zero)
        {
            transform.position = checkpointPos;
        }
        LeanTween.value(gameObject, 1, 0, 2).setOnUpdate((float val) =>
        {
            panelImage.color = new Color(0, 0, 0, val);
        }).setOnComplete(o => {
            GetComponent<MovementControllerTest>().enabled = true;
            GetComponent<CameraController>().enabled = true;
        });
    }
}
