using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Draw : MonoBehaviour
{
    //TP2 Leonardo Gonzalez Chiavassa
    private PlayerInputActions _input;
    private IDrawingSurface _currentSurface;
    [SerializeField] private LayerMask firstPersonLayer;

    private void Awake() {
        _input = GameManager.Input;
    }

    void Update() {
        if (!_input.BookActions.DrawButton.IsPressed()) {
            if (_currentSurface is not null) {
                _currentSurface.FinishDrawing();
                
                _currentSurface = null;
            }

            return;
        }
        
        
        Ray ray = Camera.main.ScreenPointToRay(_input.BookActions.MousePosition.ReadValue<Vector2>());
        RaycastHit[] hits = Physics.RaycastAll(ray, 5f);
        Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        foreach (var hitt in hits) {
            if (hitt.transform.gameObject.TryGetComponent(out DrawingSurfaceSpells surface)) {
                _currentSurface = surface;
                _currentSurface.NotifyPosition(hitt.textureCoord);
                break;
            }
            if (hitt.transform.gameObject.TryGetComponent(out DrawingSurfacePuzzle surfacePuzzle)) {
                _currentSurface = surfacePuzzle;
                _currentSurface.NotifyPosition(hitt.textureCoord);
                break;
            }
        }
        
        
    }
}
