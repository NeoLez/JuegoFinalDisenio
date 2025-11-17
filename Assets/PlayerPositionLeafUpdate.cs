using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLeafUpdate : MonoBehaviour
{
    public List<Material> materials = new();
    void Update()
    {
        foreach (Material material in materials) {
            material.SetVector("_PlayerPosition", transform.position);
        }
    }
}
