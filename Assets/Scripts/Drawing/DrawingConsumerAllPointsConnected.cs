using UnityEngine;

public class DrawingConsumerAllPointsConnected : DrawingConsumer{
    //TP2 Leonardo Gonzalez Chiavassa
    public override bool Consume(Drawing drawing, int pointAmount) {
        bool[] containsPoint = new bool[pointAmount];
        foreach (var line in drawing.lines) {
            containsPoint[line.firstByte] = true;
            containsPoint[line.secondByte] = true;
        }

        bool containsAll = true;
        foreach (var contains in containsPoint) {
            if (!contains) {
                containsAll = false;
                break;
            }
        }
        
        return containsAll;
    }
}