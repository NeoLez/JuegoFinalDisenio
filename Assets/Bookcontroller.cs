using UnityEngine;
using TMPro;
using System.Collections;

public class BookController : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject bookPanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI continueText;

    [Header("Configuración")]
    public bool autoCloseWhenEmpty = true;
    public float blinkSpeed = 0.6f;

    private bool isOpen = false;
    private bool hasDialogue = false;
    private bool isTyping = false;
    private string fullText = "";

    public static BookController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        bookPanel.SetActive(false);
        if (continueText != null)
            continueText.gameObject.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        hasDialogue = true;
        dialogueText.text = text;
        isTyping = false;
        HideContinue();
        OpenBook();
    }

    public void ShowDialogueTypewriter(string text, float speed = 0.04f)
    {
        hasDialogue = true;
        fullText = text;
        HideContinue();
        OpenBook();
        StopAllCoroutines();
        StartCoroutine(TypewriterEffect(text, speed));
    }

    public void SkipOrAdvance()
    {
        if (isTyping)
            CompleteText();
    }

    public void EndDialogue()
    {
        hasDialogue = false;
        isTyping = false;
        StopAllCoroutines();
        HideContinue();

        if (autoCloseWhenEmpty)
            CloseBook();
    }

    public void ShowContinuePrompt()
    {
        if (continueText == null) return;
        continueText.gameObject.SetActive(true);
        StartCoroutine(BlinkContinue());
    }

    void HideContinue()
    {
        if (continueText == null) return;
        continueText.gameObject.SetActive(false);
    }

    void CompleteText()
    {
        StopAllCoroutines();
        isTyping = false;
        dialogueText.text = fullText;
        ShowContinuePrompt();
    }

    void OpenBook()
    {
        if (isOpen) return;
        isOpen = true;
        bookPanel.SetActive(true);
    }

    void CloseBook()
    {
        if (!isOpen) return;
        isOpen = false;
        bookPanel.SetActive(false);
        dialogueText.text = "";
    }
    
    public void ForceClose()
    {
        EndDialogue();
    }

    IEnumerator TypewriterEffect(string text, float speed)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in text)
        {
            if (!isTyping) yield break;
            dialogueText.text += c;
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
        ShowContinuePrompt();
    }

    IEnumerator BlinkContinue()
    {
        while (continueText.gameObject.activeSelf)
        {
            continueText.alpha = 1f;
            yield return new WaitForSeconds(blinkSpeed);
            continueText.alpha = 0f;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public bool IsOpen => isOpen;
    public bool HasActiveDialogue => hasDialogue;
    public bool IsTyping => isTyping;
}