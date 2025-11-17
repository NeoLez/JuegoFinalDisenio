using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomTrigger : MonoBehaviour
{
    public VenomVisualEffect cameraEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraEffect.activeEffect = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraEffect.activeEffect = false;
    }
}
