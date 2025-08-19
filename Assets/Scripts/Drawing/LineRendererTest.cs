using UnityEngine;

public class LineRendererTest : MonoBehaviour {
    //TP2 Leonardo Gonzalez Chiavassa
    [SerializeField] private GameObject point;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private bool add;

    private void Update() {
        if (add) {
            add = false;
            LineRenderer.AddPoint(point.transform.localPosition);
        }
    }
}