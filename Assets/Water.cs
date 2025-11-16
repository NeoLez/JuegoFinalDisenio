using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private List<Rigidbody> rigidbodiesInWater = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodiesInWater.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodiesInWater.Remove(rb);
        }
    }

    private void FixedUpdate()
    {
        foreach (var rb in rigidbodiesInWater)
        {
            var waterBehaviour = rb.GetComponent<WaterBehaviour>();
            if (waterBehaviour == null) continue;

            float objectY = rb.position.y;
            float surfaceY = waterBehaviour.waterSurfaceY;
            
            float depth = surfaceY - objectY;

            if (depth > 0f) 
            {
                float buoyancyForceMagnitude = Mathf.Min(waterBehaviour.buoyancyForce * depth, waterBehaviour.buoyancyForce * 2f);
                
                rb.AddForce(Vector3.up * buoyancyForceMagnitude, ForceMode.Acceleration);
                
                rb.velocity *= (1f - waterBehaviour.waterDrag * Time.fixedDeltaTime);
                rb.angularVelocity *= (1f - waterBehaviour.angularDrag * Time.fixedDeltaTime);
                
                if (rb.velocity.y > 2f)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 2f, rb.velocity.z);
                }
            }
            else if (depth > -0.3f && depth <= 0f)
            {
                float stabilizationForce = waterBehaviour.buoyancyForce * 0.3f;
                rb.AddForce(Vector3.up * stabilizationForce, ForceMode.Acceleration);
                
                rb.velocity = new Vector3(
                    rb.velocity.x * (1f - waterBehaviour.waterDrag * 0.5f * Time.fixedDeltaTime),
                    rb.velocity.y * 0.9f, 
                    rb.velocity.z * (1f - waterBehaviour.waterDrag * 0.5f * Time.fixedDeltaTime)
                );
            }
        }
    }
}