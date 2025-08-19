using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class DrawingSurfacePuzzle : MonoBehaviour, IDrawingSurface {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] public DrawingPoint[] points;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Material circleMaterial;
    [SerializeField] private List<Line> tuples = new(4);
    [SerializeField] private float lineOffset;
    [SerializeField] private AudioClip lineDrawSound;
    private byte? _lastPoint;
    
    [SerializeField] private float lineWidth;
    [SerializeField] private float circleLineWidth;
    private MeshFilter _lineMeshFilter;
    [SerializeField] private int circleResolution;
    private MeshFilter _circlesMeshFilter;

    private DrawingConsumer consumer;
    public event Action OnPuzzleSolved;
    private void Awake()
    {
        consumer = GetComponent<DrawingConsumer>();
        
        GameObject linesObject = new GameObject();
        linesObject.transform.SetParent(transform);
        linesObject.transform.localPosition = Vector3.back * lineOffset;
        linesObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _lineMeshFilter = linesObject.AddComponent<MeshFilter>();
        var meshRenderer = linesObject.AddComponent<MeshRenderer>();
        meshRenderer.material = lineMaterial;
        //linesObject.gameObject.layer = LayerMask.NameToLayer("FirstPerson");
        
        GameObject circlesObject = new GameObject();
        circlesObject.transform.SetParent(transform);
        circlesObject.transform.localPosition = Vector3.back * lineOffset;
        circlesObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _circlesMeshFilter = circlesObject.AddComponent<MeshFilter>();
        var circlesMeshRenderer = circlesObject.AddComponent<MeshRenderer>();
        circlesMeshRenderer.material = circleMaterial;
        //circlesObject.layer = LayerMask.NameToLayer("FirstPerson");
        
        DrawCircles();
    }

    public void FinishDrawing() {
        Drawing drawing = new Drawing(tuples.ToHashSet().ToArray());
        
        if(consumer.Consume(drawing, points.Length))
            OnPuzzleSolved?.Invoke();

        tuples.Clear();
        _lastPoint = null;

        _lineMeshFilter.mesh = new Mesh();
        lastAccepted = null;
    }

    private Vector2? lastAccepted;
    public void NotifyPosition(Vector2 position) {
        Vector2 unscaled = PosToUnscaled(position);
        
        for (byte i = 0; i< points.Length; i++) {
            Vector2 pointUnscaled = points[i].position;
            if (Vector2.Distance(pointUnscaled, unscaled) <= points[i].size) {
                points[i].selected = true;
                if (_lastPoint is not null) {
                    if (_lastPoint != i) {
                        Line newTuple = new Line(_lastPoint.Value, i);
                        //if (!_tuples.Contains(newTuple)) {
                            tuples.Add(newTuple);
                            GameManager.AudioSystem.PlaySound(lineDrawSound);
                        //}
                    }
                }
                _lastPoint = i;
                break;
            }
        }
        
        DrawGraphics();
    }

    private void DrawGraphics() {
        var vertices = new Vector3[tuples.Count * 4];
        int[] indices = new int[tuples.Count * 6];
        
        for(int i = 0; i < tuples.Count; i++) {
            var tuple = tuples[i];
            
            Vector2 pos1 = points[tuple.firstByte].position;
            Vector2 pos2 = points[tuple.secondByte].position;
            Vector2 distance = (pos2 - pos1).normalized;
            Vector2 perpendicular = new Vector2(-distance.y, distance.x);
            perpendicular *= lineWidth/2;
            
            vertices[i * 4] = (pos1 + distance * points[tuple.firstByte].size + perpendicular).Swizzle_xy0();
            vertices[i * 4 + 1] = (pos1 + distance * points[tuple.firstByte].size - perpendicular).Swizzle_xy0();
            vertices[i * 4 + 2] = (pos2 - distance * points[tuple.secondByte].size + perpendicular).Swizzle_xy0();
            vertices[i * 4 + 3] = (pos2 - distance * points[tuple.secondByte].size - perpendicular).Swizzle_xy0();

            indices[i * 6] = i * 4;
            indices[i * 6 + 1] = i * 4 + 2;
            indices[i * 6 + 2] = i * 4 + 1;
            indices[i * 6 + 3] = i * 4 + 1;
            indices[i * 6 + 4] = i * 4 + 2;
            indices[i * 6 + 5] = i * 4 + 3;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        _lineMeshFilter.mesh = mesh;
    }

    [SerializeField] private Vector2 posToWorld;
    [SerializeField] private Transform objec;
    private void Update() {
        DrawCircles();
        CheckForBarriers();
    }

    private void CheckForBarriers() {
        var a = points.Select(((point, i) => {
            return PosToWorld(point.position);
        })).ToArray();
        
        foreach (var tuple in tuples) {
            Vector3 firstPos = a[tuple.firstByte];
            Vector3 secondPos = a[tuple.secondByte];
            Vector3 diff = secondPos - firstPos;
            Ray ray = new Ray(firstPos, diff);
            if (Physics.Raycast(ray, diff.magnitude, LayerMask.GetMask("DrawingBarrier"))) {
                tuples.Clear();
                break;
            }
        }
    }

    private void DrawCircles() {
        var circlesVertices = new Vector3[points.Length * 2 * circleResolution];
        int[] circlesIndices = new int[points.Length * 6 * circleResolution];
        for(int i = 0; i < points.Length; i++) {
            int vertexOffset = circleResolution * 2 * i;
            int indexOffset = circleResolution * 6 * i;
            for (int j = 0; j < circleResolution; j++) {
                float angle = (360.0f / circleResolution) * j;
                Vector2 v = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                circlesVertices[vertexOffset + j * 2] = v * (points[i].size + circleLineWidth / 2f) + points[i].position;
                circlesVertices[vertexOffset + j * 2 + 1] = v * (points[i].size - circleLineWidth / 2f) + points[i].position;

                circlesIndices[indexOffset + j * 6] = vertexOffset + (j * 2) % (circleResolution * 2);
                circlesIndices[indexOffset + j * 6 + 1] = vertexOffset + (j * 2 + 2) % (circleResolution * 2);
                circlesIndices[indexOffset + j * 6 + 2] = vertexOffset + (j * 2 + 1) % (circleResolution * 2);
                circlesIndices[indexOffset + j * 6 + 3] = vertexOffset + (j * 2 + 1) % (circleResolution * 2);
                circlesIndices[indexOffset + j * 6 + 4] = vertexOffset + (j * 2 + 2) % (circleResolution * 2);
                circlesIndices[indexOffset + j * 6 + 5] = vertexOffset + (j * 2 + 3) % (circleResolution * 2);
            }
        }
        
        Mesh circlesMesh = new Mesh();
        circlesMesh.vertices = circlesVertices;
        circlesMesh.triangles = circlesIndices;
        circlesMesh.RecalculateNormals();
        _circlesMeshFilter.mesh = circlesMesh;
    }

    private Vector2 PosToUnscaled(Vector2 v) {
        return new Vector2((v.x - 0.5f) * transform.localScale.x, (v.y - 0.5f) * transform.localScale.y);
    }

    public Vector3 PosToWorld(Vector2 v) {
        Vector2 unscaled = v.Swizzle_xy0();
        float3x3 matrix = new float3x3(transform.right, transform.up, transform.forward);
        float3x3 matrixInverse = math.inverse(matrix);
        float3 result = math.mul(new float3(unscaled.x, unscaled.y, 0), matrixInverse);
        return new Vector3(result.x, result.y, result.z) + transform.position;
    }
}
