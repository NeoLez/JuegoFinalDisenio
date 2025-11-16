using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public float buoyancyForce = 15f;
    
    [Range(0f, 1f)]
    public float waterDrag = 0.95f;
    
    [Range(0f, 1f)]
    public float angularDrag = 0.8f;
    
    public float waterSurfaceY = 0f;
    
    public float densityMultiplier = 1f;
}