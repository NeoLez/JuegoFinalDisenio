using System;
using UnityEngine;

[Serializable]
public class Line {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] private byte byte1;
    [SerializeField] private byte byte2;
    private readonly int _hashCode;
    
    public byte firstByte => byte1;
    public byte secondByte => byte2;

    public Line(byte byte1, byte byte2) {
        this.byte1 = byte1;
        this.byte2 = byte2;
        _hashCode = byte1.GetHashCode() ^ byte2.GetHashCode();
    }

    public override bool Equals(object obj) {
        return obj is Line other &&
               ((byte1 == other.byte1 && byte2 == other.byte2) ||
                (byte1 == other.byte2 && byte2 == other.byte1));
    }

    public override int GetHashCode() {
        return _hashCode;
    }

    public override string ToString() {
        return $"[{byte1}, {byte2}]";
    }
}