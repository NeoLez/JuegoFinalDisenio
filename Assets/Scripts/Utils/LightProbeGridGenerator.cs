using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class LightProbeGridGenerator : MonoBehaviour
{
    public Vector3 gridSize = new Vector3(10, 3, 10); // Size of volume (meters)
    public Vector3Int probeCount = new Vector3Int(5, 2, 5); // How many probes in each axis
    public LightProbeGroup probeGroup;

    [ContextMenu("Generate Probes")]
    public void GenerateProbes()
    {
        if (probeGroup == null)
        {
            probeGroup = GetComponent<LightProbeGroup>();
            if (probeGroup == null)
            {
                probeGroup = gameObject.AddComponent<LightProbeGroup>();
            }
        }

        Vector3[] probes = new Vector3[probeCount.x * probeCount.y * probeCount.z];
        int index = 0;

        for (int x = 0; x < probeCount.x; x++)
        {
            for (int y = 0; y < probeCount.y; y++)
            {
                for (int z = 0; z < probeCount.z; z++)
                {
                    float px = (x / (float)(probeCount.x - 1));
                    float py = (y / (float)(probeCount.y - 1));
                    float pz = (z / (float)(probeCount.z - 1));
                    probes[index++] = new Vector3(px, py, pz) - Vector3.one * 0.5f; // Centered
                }
            }
        }

        probeGroup.probePositions = probes;
        Debug.Log("Light probes generated: " + probes.Length);
    }
}
#endif