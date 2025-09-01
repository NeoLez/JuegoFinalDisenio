using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    public GameObject PauseMenu;
    public bool paused = false;
    public GameObject SalirConfirmar;
    public GameObject OpcionConfirmar;

    [Header("Sonidos")]
    public AudioClip sonidoPausa;
    public AudioClip sonidoReanudar;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Input.Pause.Pause.performed += TriggerMenu;
    }

    private void TriggerMenu(InputAction.CallbackContext ctx) {
        if (!paused)
        {
            if (sonidoPausa != null)
                GameManager.AudioSystem.PlaySound(sonidoPausa);

            PauseMenu.SetActive(true);
            paused = true;

            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GameManager.Input.Movement.Disable();
            GameManager.Input.CameraMovement.Disable();
            GameManager.Input.BookActions.Disable();
            GameManager.Input.Scanner.Disable();
            GameManager.Input.Drag.Disable();
            GameManager.Input.CardUsage.Disable();

            AudioSource[] songs = FindObjectsOfType<AudioSource>();
            foreach (AudioSource s in songs)
            {
                if (s != audioSource)
                {
                    s.Pause();
                }
            }
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
        

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.Input.Movement.Enable();
        GameManager.Input.CameraMovement.Enable();
        GameManager.Input.BookActions.Enable();
        GameManager.Input.Scanner.Enable();
        GameManager.Input.Drag.Enable();
        GameManager.Input.CardUsage.Enable();
        
        AudioSource[] songs = FindObjectsOfType<AudioSource>();
        foreach (AudioSource s in songs)
        {
            if (s != audioSource)
            {
                s.Play();
            }
        }
    }
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(0); 
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("saliste xd");
    }
}
