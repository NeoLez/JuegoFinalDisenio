using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class LineRenderer : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    private List<Vector3> vertices = new();
    private List<int> triangles = new();
    private List<Vector2> points = new();

    [SerializeField] private float lineWidth;

    private MeshFilter _lineMeshFilter;

    private void Awake() {
        _lineMeshFilter = GetComponent<MeshFilter>();
    }

    private void RecalculateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        _lineMeshFilter.mesh = mesh;
    }

    public void ClearMesh() {
        vertices.Clear();
        triangles.Clear();
        points.Clear();
        RecalculateMesh();
    }

    private int indUp, indDown;
    public void AddPoint(Vector2 point) {
        points.Add(point);
        if (points.Count == 1) return;
        
        Vector2 dirBC = points[^1] - points[^2];
        float BCLength = dirBC.magnitude;
        dirBC.Normalize();
        Vector2 dirBCright = new Vector2(-dirBC.y, dirBC.x);
        if (points.Count == 2) {
            vertices.Add(points[0] + dirBCright * lineWidth);
            vertices.Add(points[0] - dirBCright * lineWidth);
            vertices.Add(points[1] + dirBCright * lineWidth);
            vertices.Add(points[1] - dirBCright * lineWidth);
            triangles.Add(0);
            triangles.Add(2);
            triangles.Add(1);
            triangles.Add(1);
            triangles.Add(2);
            triangles.Add(3);
            RecalculateMesh();
            indUp = 0;
            indDown = 1;
            return;
        }
        // A--------B------------C
        Vector2 dirAB = points[^2] - points[^3];
        float ABLength = dirAB.magnitude;
        dirAB.Normalize();
        Vector2 dirAC = points[^1] - points[^3];
        float ACLength = dirAC.magnitude;
        dirAC.Normalize();
        

        var crossProductDirection = dirAC.x * dirAB.y - dirAC.y * dirAB.x;

        Vector2 dirABright = new Vector2(-dirAB.y, dirAB.x);
        dirABright.Normalize();
        
        if (dirAB.x == 0) {
            dirAB.x = 0.01f;
        }
        if (dirBC.x == 0) {
            dirBC.x = 0.01f;
        }

        if (crossProductDirection == 0 || Vector2.Angle(dirAB, dirBC) > 170) {
            if (Vector2.Angle(dirAB, dirBC) > 90) {
                vertices.Add(points[^1] + dirBCright * lineWidth);
                vertices.Add(points[^1] - dirBCright * lineWidth);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
            }
            else {
                vertices.Add(points[^1] + dirBCright * lineWidth);
                vertices.Add(points[^1] - dirBCright * lineWidth);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
            }
            
            
            
        } else if (crossProductDirection < 0) {
            Vector2 pointAB = points[^2] + dirABright * lineWidth;
            Vector2 pointBC = points[^2] + dirBCright * lineWidth;
            float terminoIndependienteAB = pointAB.y - (dirAB.y / dirAB.x) * pointAB.x;
            float terminoIndependienteBC = pointBC.y - (dirBC.y / dirBC.x) * pointBC.x;
            float x = (terminoIndependienteBC - terminoIndependienteAB) / (dirAB.y/dirAB.x - dirBC.y/dirBC.x);

            vertices[^2] = new Vector3(x, (dirAB.y / dirAB.x) * x + terminoIndependienteAB);
            vertices.Add(points[^2] - dirBCright * lineWidth);
            vertices.Add(points[^1] + dirBCright * lineWidth);
            vertices.Add(points[^1] - dirBCright * lineWidth);
            
            triangles.Add(vertices.Count - 5);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 5);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
        }else if (crossProductDirection > 0) {
            Vector2 pointAB = points[^2] - dirABright * lineWidth;
            Vector2 pointBC = points[^2] - dirBCright * lineWidth;
            float terminoIndependienteAB = pointAB.y - (dirAB.y / dirAB.x) * pointAB.x;
            float terminoIndependienteBC = pointBC.y - (dirBC.y / dirBC.x) * pointBC.x;
            float x = (terminoIndependienteBC - terminoIndependienteAB) / (dirAB.y/dirAB.x - dirBC.y/dirBC.x);

            vertices[^1] = new Vector3(x, (dirAB.y / dirAB.x) * x + terminoIndependienteAB);
            vertices.Add(points[^2] + dirBCright * lineWidth);
            vertices.Add(points[^1] + dirBCright * lineWidth);
            vertices.Add(points[^1] - dirBCright * lineWidth);
            
            triangles.Add(vertices.Count - 5);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
        }
        RecalculateMesh();
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < vertices.Count; i++) {
            var vertex = vertices[i];
            Gizmos.color = new Color(0, i/5.0f, 0);
            Gizmos.DrawRay(vertex + transform.position, -transform.forward);
        }
    }
}