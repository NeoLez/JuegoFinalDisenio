using System.Collections.Generic;
using StatusEffects;
using UnityEngine;

public class BurnEffectPuzzleTorch : MonoBehaviour, IEffectBehaviour{
    [SerializeField] private bool shouldBeOn;
    [SerializeField] private bool isOn;
    [SerializeField] private List<GameObject> objects = new();

    public bool IsInIntendedState() {
        return shouldBeOn == isOn;
    }

    public StatusEffectsType GetType() {
        return StatusEffectsType.BURN;
    }

    public void Enable() {
        isOn = true;
        objects.ForEach(o => o.SetActive(true));
    }

    public void Disable() {
        isOn = false;
        objects.ForEach(o => o.SetActive(false));
    }

    public void Tick() {
    }
}