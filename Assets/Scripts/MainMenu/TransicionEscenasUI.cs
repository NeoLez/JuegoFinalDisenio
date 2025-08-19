using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscenasUI : MonoBehaviour
{
    //TP2 Enzo Francisco Melidoni
    public static TransicionEscenasUI instance;

    [Header("Disolver")]
    public CanvasGroup disolverCanvasGroup;
    public float tiempoDisolverEntrada;
    public float tiempoDisolverSalida;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DisolverEntrada();
    }

    private void DisolverEntrada()
    {
        LeanTween.alphaCanvas(disolverCanvasGroup, 0f, tiempoDisolverEntrada).setOnComplete(() =>
        {
            disolverCanvasGroup.blocksRaycasts = false;
            disolverCanvasGroup.interactable = false;
        });
    }

    public void DisolverSalida(string nombreEscena)
    {
        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        LeanTween.alphaCanvas(disolverCanvasGroup, 1f, tiempoDisolverSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(nombreEscena);
        });
    }

}