using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawingPuzzleArea : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] [ColorUsage(true, true)] public Color crateBeamColor;
    [SerializeField] private float xSize;
    [FormerlySerializedAs("ySize")] [SerializeField] private float zSize;

    [SerializeField] private Transform[] objectsPos;

    [SerializeField] private DrawingSurfacePuzzle surface;

    [SerializeField] public Vector3 rotation;
    [SerializeField] private Vector3 rotationSpeed;
    [SerializeField] private GameObject objectToRotate;

    private void Update() {
        if (objectToRotate != null) {
            Vector3 currentFrameRotation = rotationSpeed * Time.deltaTime;
            
            
            if (currentFrameRotation.x > rotation.x) currentFrameRotation.x = rotation.x;
            if (currentFrameRotation.y > rotation.y) currentFrameRotation.y = rotation.y;
            if (currentFrameRotation.z > rotation.z) currentFrameRotation.z = rotation.z;
            
            rotation -= currentFrameRotation;
            objectToRotate.transform.rotation *= Quaternion.Euler(currentFrameRotation);
        }
        
        for (int i = 0; i < objectsPos.Length; i++) {
            float3x3 matrix = new float3x3(new float3(transform.forward), new float3(transform.up), new float3(transform.right));
            float3 localPos = math.mul(new float3(objectsPos[i].position - transform.position), matrix) / new float3(xSize, 1, zSize);
            surface.points[i].position = new Vector2(localPos.x, localPos.z);
        }
    }

    public bool IsRotating() {
        return rotation.sqrMagnitude > 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        foreach (var objPos in objectsPos) {
            Gizmos.DrawSphere(Vector3.ProjectOnPlane(objPos.transform.position - transform.position, transform.up) + transform.position, 0.05f);
        }
    }
}