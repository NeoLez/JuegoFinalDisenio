using UnityEngine;
using Facts;
using System.Collections.Generic;

public class SpellBlockerEnemy : MonoBehaviour
{
    [Header("Blocked Spells Configuration")]
    [SerializeField] private List<SpellType> blockedSpells = new List<SpellType>();
    
    [Header("Visual Feedback")]
    [SerializeField] private GameObject blockedSpellIndicator; // esto es para el icono sobre el enemigo
    
    private bool isActive = false;
    private Transform player;
    
    private void Start()
    {
        player = GameManager.Player.transform;
        SubscribeToSpellEvents();
    }
    
    // Método para activar el enemigo (cuando entres a la sala)
    public void Activate()
    {
        isActive = true;
        if (blockedSpellIndicator != null)
        {
            blockedSpellIndicator.SetActive(true);
        }
    }
    
    // Método para desactivar el enemigo (si salis de la sala)
    public void Deactivate()
    {
        isActive = false;
        
        if (blockedSpellIndicator != null)
        {
            blockedSpellIndicator.SetActive(false);
        }
    }
    
    private void SubscribeToSpellEvents()
    {
        Events.ON_PLAYER_USE_DASH_SELF.Subscribe(OnDashSelfUsed);
        Events.ON_PLAYER_USE_DASH.Subscribe(OnDashThrowUsed);
        Events.ON_PLAYER_USE_FIRE.Subscribe(OnFireUsed);
        Events.ON_PLAYER_USE_FREEZE.Subscribe(OnIceUsed);
    }
    
    private void OnDashSelfUsed(Unit _)
    {
        if (isActive && blockedSpells.Contains(SpellType.DashSelf))
        {
            KillPlayer();
        }
    }
    
    private void OnDashThrowUsed(Unit _)
    {
        if (isActive && blockedSpells.Contains(SpellType.DashThrow))
        {
            KillPlayer();
        }
    }
    
    private void OnFireUsed(Unit _)
    {
        if (isActive && blockedSpells.Contains(SpellType.Fire))
        {
            KillPlayer();
        }
    }
    
    private void OnIceUsed(Unit _)
    {
        if (isActive && blockedSpells.Contains(SpellType.Ice))
        {
            KillPlayer();
        }
    }
    
    private void KillPlayer()
    {
        Debug.Log($"no queremos un segundo pintor aleman.....");
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Die();
        }
    }
    
    private void OnDestroy()
    {
        Events.ON_PLAYER_USE_DASH_SELF.Unsubscribe(OnDashSelfUsed);
        Events.ON_PLAYER_USE_DASH.Unsubscribe(OnDashThrowUsed);
        Events.ON_PLAYER_USE_FIRE.Unsubscribe(OnFireUsed);
        Events.ON_PLAYER_USE_FREEZE.Unsubscribe(OnIceUsed);
    }
}

public enum SpellType
{
    DashSelf,      
    DashThrow,     
    Fire,          
    Ice,           
    Levitation     
}