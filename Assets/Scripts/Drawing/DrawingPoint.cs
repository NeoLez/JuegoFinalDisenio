using System;
using UnityEngine;

[Serializable]
public class DrawingPoint {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] public Vector2 position;
    [SerializeField] public float size;
    public bool selected;

    public DrawingPoint(Vector2 position, float size) {
        this.position = position;
        this.size = size;
    }
}
