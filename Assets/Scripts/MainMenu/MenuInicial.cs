using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    [Header("Nombre de la escena")]
    public string nombreEscenaAJugar = "Base 5L";

    [Header("Música")]
    public AudioSource musica;
    public float duracionFade = 2f;

    public void Jugar()
    {
        TransicionEscenasUI.instance.DisolverSalida(nombreEscenaAJugar);
        
        StartCoroutine(FadeOutMusica());
    }

    public void Prototipado()
    {
        SceneManager.LoadScene(2);
    }

    private IEnumerator FadeOutMusica()
    {
        float tiempo = 0f;
        float volumenInicial = musica.volume;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            musica.volume = Mathf.Lerp(volumenInicial, 0f, tiempo / duracionFade);
            yield return null;
        }

        musica.volume = 0f;
        musica.Stop();
    }

    public void Salir()
    {
        Debug.Log("Saliste xd");
        Application.Quit();
    }
}