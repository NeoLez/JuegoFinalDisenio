using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    [SerializeField] private int resolution;
    [SerializeField] private float sideLength;
    [SerializeField] private MeshFilter meshFilter;
    void Start()
    {   
        Mesh mesh = new Mesh();
        float distanceBetweenPoints = sideLength / (resolution-1);

        Vector3[] vertices = new Vector3[resolution*resolution];
        int[] triangles = new int[(resolution-1)*(resolution-1)*6];

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                vertices[y * resolution + x] = new Vector3(distanceBetweenPoints*y,0,distanceBetweenPoints*x);
            }
        }
        for (int y = 0; y < resolution-1; y++)
        {
            for (int x = 0; x < resolution-1; x++)
            {
                triangles[(y*(resolution-1) + x)*6] = y*resolution + x;
                triangles[(y * (resolution - 1) + x) * 6+1] = y*resolution + x + 1;
                triangles[(y * (resolution - 1) + x) * 6+2] = (y+1)*resolution + x;
                triangles[(y * (resolution - 1) + x) * 6+3] = y*resolution + x + 1;
                triangles[(y * (resolution - 1) + x) * 6+4] = (y+1)*resolution + x + 1;
                triangles[(y * (resolution - 1) + x)* 6+5] = (y+1)*resolution + x;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}
