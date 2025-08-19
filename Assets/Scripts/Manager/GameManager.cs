using UnityEngine;

public class GameManager {
    //TP2 Belen Mounier
    public static GameObject Player;
    public static PlayerInputActions Input;
    public static AudioSystem AudioSystem;
    public static Camera MainCamera;
    public static float CamFOV;

    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateInput() {
        Input = new PlayerInputActions();
        Input.CameraMovement.Enable();
        Input.BookActions.Enable();
        Input.Movement.Enable();
        Input.CardUsage.Enable();
        Input.Drag.Enable();
        Input.Scanner.Enable();
        Input.Cheats.Enable();
        Input.WorldInteractions.Enable();

        Input.Cheats.UnlockAllSpells.started += (ctx) => {
            DrawingPatternDatabase.UnlockAllSpells();
        };

        AudioSystem = new AudioSystem();
    }
}
