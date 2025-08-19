using System;
using UnityEngine;

[Serializable]
public class Drawing {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] public Line[] lines;
    private readonly int _hashCode;

    public Drawing(Line[] lines) {
        this.lines = lines;
        
        foreach (var line in lines) {
            if (_hashCode == 0)
                _hashCode = line.GetHashCode();
            else
                _hashCode ^= line.GetHashCode();
        }
    }
    
    public override bool Equals(object obj) {
        if (obj is Drawing) {
            var drawing = (Drawing)obj;
            if (drawing.lines.Length != lines.Length)
                return false;
            
            bool areEqual = true;

            foreach (var line in drawing.lines) {
                bool found = false;
                foreach (var line2 in lines) {
                    if (line.Equals(line2)) {
                        found = true;
                        break;
                    }
                }

                if (!found) {
                    areEqual = false;
                    break;
                }
            }
            
            return areEqual;
        }
        
        return false;
    }

    public override int GetHashCode() {
        return _hashCode;
    }
}
