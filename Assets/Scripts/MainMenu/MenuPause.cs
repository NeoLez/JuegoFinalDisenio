using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool paused = false;
    public GameObject SalirConfirmar;
    public GameObject OpcionConfirmar;

    [Header("Sonidos")]
    public AudioClip sonidoPausa;
    public AudioClip sonidoReanudar;

    void Start()
    {
        GameManager.Input.Pause.Pause.performed += TriggerMenu;
    }

    private void TriggerMenu(InputAction.CallbackContext ctx) {
        if (!paused)
        {
            if (BookController.Instance != null)
                BookController.Instance.ForceClose();

            PauseMenu.SetActive(true);
            paused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GameManager.Input.Movement.Disable();
            GameManager.Input.CameraMovement.Disable();
            GameManager.Input.BookActions.Disable();
            GameManager.Input.Scanner.Disable();
            GameManager.Input.Drag.Disable();
            GameManager.Input.CardUsage.Disable();
            GameManager.Input.WorldInteractions.Disable();

            GameManager.AudioSystem.PauseAll();
            if (sonidoPausa != null)
                GameManager.AudioSystem.PlaySound(sonidoPausa);
        }
        else
        {
            if (sonidoReanudar != null)
                GameManager.AudioSystem.PlaySound(sonidoReanudar);
            resume();
        }
    }

    public void resume()
    {
        PauseMenu.SetActive(false);
        SalirConfirmar.SetActive(false);
        OpcionConfirmar.SetActive(false);
        paused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.Input.Movement.Enable();
        GameManager.Input.CameraMovement.Enable();
        GameManager.Input.BookActions.Enable();
        GameManager.Input.Scanner.Enable();
        GameManager.Input.Drag.Enable();
        GameManager.Input.CardUsage.Enable();
        GameManager.Input.WorldInteractions.Enable();

        GameManager.AudioSystem.ResumeAll();
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("saliste xd");
    }
}