using UnityEngine;

public abstract class DrawingConsumer : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    public abstract bool Consume(Drawing drawing, int amountOfPoints);
}