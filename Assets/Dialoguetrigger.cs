using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Diálogos del Altar")]
    [TextArea(2, 6)]
    public string[] dialogueLines = {
        "Bienvenido al altar de las sombras...",
        "Este libro guarda los secretos de los antiguos.",
        "Solo los dignos pueden leer sus páginas."
    };

    [Header("Configuración")]
    public bool useTypewriter = true;
    [Range(0.01f, 0.1f)]
    public float typewriterSpeed = 0.04f;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    void Update()
    {
        if (!playerInRange || !Input.GetKeyDown(KeyCode.F)) return;

        if (!dialogueActive)
        {
            StartDialogue();
            return;
        }

        if (BookController.Instance.IsTyping)
            BookController.Instance.SkipOrAdvance();
        else
            NextLine();
    }

    void StartDialogue()
    {
        if (dialogueLines.Length == 0) return;
        dialogueActive = true;
        currentLine = 0;
        ShowCurrentLine();
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
            ShowCurrentLine();
        else
            EndDialogue();
    }

    void ShowCurrentLine()
    {
        if (BookController.Instance == null) return;

        if (useTypewriter)
            BookController.Instance.ShowDialogueTypewriter(dialogueLines[currentLine], typewriterSpeed);
        else
            BookController.Instance.ShowDialogue(dialogueLines[currentLine]);
    }

    void EndDialogue()
    {
        dialogueActive = false;
        currentLine = 0;
        if (BookController.Instance != null)
            BookController.Instance.EndDialogue();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (dialogueActive)
                EndDialogue();
        }
    }
}