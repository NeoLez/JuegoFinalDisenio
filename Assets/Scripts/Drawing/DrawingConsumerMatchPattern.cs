using System.Collections.Generic;
using UnityEngine;

public class DrawingConsumerMatchPattern : DrawingConsumer
{
    //TP2 Leonardo Gonzalez Chiavassa
    public List<DrawingPatternSO> patterns;
    public override bool Consume(Drawing drawing, int amountOfPoints)
    {
        foreach (var pattern in patterns) 
        {
            if (pattern.drawing.Equals(drawing))
            {
                return true;
            }
        }
        return false;
    }
}
