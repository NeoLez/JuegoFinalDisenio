using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TerrainScanner : MonoBehaviour
{
    public GameObject terrainScanPrefab;
    public float duration = 10;
    public float size = 500;
    public float cooldown = 10f;
    private float lastUsed = float.MinValue;

    private void Start() {
        GameManager.Input.Scanner.Scan.started += ActivateSpawnScanner;
    }

    void ActivateSpawnScanner(InputAction.CallbackContext ctx) {
        if (Time.time - lastUsed <= cooldown)
            return;

        lastUsed = Time.time;
        
        GameObject terrainScan = Instantiate(terrainScanPrefab, gameObject.transform.position, Quaternion.identity);
        ParticleSystem terrainScannerPS = terrainScan.GetComponentInChildren<ParticleSystem>();

        if (terrainScannerPS != null)
        {
            var main = terrainScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.LogError("the first child doesnt have a particle system");
        }
        Destroy(terrainScan, duration + 1);
    }
}
