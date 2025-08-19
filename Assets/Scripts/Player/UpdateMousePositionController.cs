using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdateMousePositionController : MonoBehaviour
{
    void Update()
    {
        Vector2 mouseDelta = GameManager.Input.BookActions.Newaction.ReadValue<Vector2>();
        Mouse.current.WarpCursorPosition(Mouse.current.position.value + mouseDelta);
    }
}
