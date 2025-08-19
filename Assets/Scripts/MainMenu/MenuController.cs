using UnityEngine;

public class MenuController : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    public GameObject menuAudio;
    public GameObject menuControles;
    public GameObject menuGraficos;
    public GameObject menuPrincipal;

    public void ToggleAudioMenu()
    {
        bool isActive = menuAudio.activeSelf;
        CloseAllMenus();
        menuAudio.SetActive(!isActive);
    }

    public void ToggleControlesMenu()
    {
        bool isActive = menuControles.activeSelf;
        CloseAllMenus();
        menuControles.SetActive(!isActive);
    }

    public void ToggleGraficosMenu()
    {
        bool isActive = menuGraficos.activeSelf;
        CloseAllMenus();
        menuGraficos.SetActive(!isActive);
        
    }
    
    public void ComebackToMenu()
    {
        CloseAllMenus();
        menuPrincipal.SetActive(true);
    }
    private void CloseAllMenus()
    {
        menuAudio.SetActive(false);
        menuControles.SetActive(false);
        menuGraficos.SetActive(false);
    }
}
