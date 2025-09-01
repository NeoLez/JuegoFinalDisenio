using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogues : MonoBehaviour
{
    private bool isPlayerInRange;
    [SerializeField] private bool isDialoguePlaying;
    [SerializeField] private bool isFillingInLine;
    private int lineIndex;
    private float typingSpeed = 0.05f;
    [SerializeField] private bool shouldStartAuto;
    [SerializeField] private bool hasAutoPlayed;
    [SerializeField] private GameObject dialogueInteraction;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextLineHint;
    [SerializeField, TextArea(3, 5)] private string[] dialogueLines;

    private Coroutine blinkCoroutine;

    private void Awake() {
        GameManager.Input.WorldInteractions.Interact.performed += PlayerInteracted;
        if (nextLineHint != null)
            nextLineHint.SetActive(false);
    }

    private void PlayerInteracted(InputAction.CallbackContext ctx) {
        if (isDialoguePlaying) {
            if (isFillingInLine) {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                isFillingInLine = false;
                ShowHint();
            }
            else {
                NextDialogueLine();
            }
        }
        else if (!shouldStartAuto && isPlayerInRange) {
            StartDialogue();
        }
    }
    
    private void StartDialogue()
    {
        if (shouldStartAuto) {
            GameManager.Input.Movement.Disable();
            GameManager.Input.CameraMovement.Disable();
            GameManager.Input.BookActions.Disable();
            GameManager.Input.Scanner.Disable();
            GameManager.Input.Drag.Disable();
            GameManager.Input.CardUsage.Disable();
        }
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
        dialogueInteraction.SetActive(false);
        lineIndex = 0;
        isFillingInLine = true;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        StopAllCoroutines();
        lineIndex++;
        HideHint();

        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
            dialoguePanel.SetActive(false);
            dialogueInteraction.SetActive(true);
            GameManager.Input.Movement.Enable();
            GameManager.Input.CameraMovement.Enable();
            GameManager.Input.BookActions.Enable();
            GameManager.Input.Scanner.Enable();
            GameManager.Input.Drag.Enable();
            GameManager.Input.CardUsage.Enable();
        }
    }
    private void EndDialogue()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        
        StartCoroutine(ShowEndHint());

        if (shouldStartAuto) 
        {
            hasAutoPlayed = true;
        }
        else {
            dialogueInteraction.SetActive(true);
        }
    }

    private IEnumerator ShowLine() {
        isFillingInLine = true;
        dialogueText.text = string.Empty;
        HideHint();

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }

        isFillingInLine = false;
        ShowHint(); 
    }

    private void ShowHint()
    {
        if (nextLineHint == null) return;

        nextLineHint.SetActive(true);
        
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
        blinkCoroutine = StartCoroutine(BlinkHint());
    }

    private void HideHint()
    {
        if (nextLineHint == null) return;

        nextLineHint.SetActive(false);

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    private IEnumerator BlinkHint()
    {
        CanvasGroup cg = nextLineHint.GetComponent<CanvasGroup>();
        if (cg == null) cg = nextLineHint.AddComponent<CanvasGroup>();

        while (true)
        {
            // Fade out
            for (float t = 1f; t >= 0; t -= Time.deltaTime * 2f)
            {
                cg.alpha = t;
                yield return null;
            }
            // Fade in
            for (float t = 0; t <= 1f; t += Time.deltaTime * 2f)
            {
                cg.alpha = t;
                yield return null;
            }
        }
    }

    private IEnumerator ShowEndHint()
    {
        ShowHint();
        yield return new WaitForSeconds(3f);
        HideHint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Player) {
            isPlayerInRange = true;
            if(shouldStartAuto) {
                if(!hasAutoPlayed)
                    StartDialogue();
            }
            else {
                dialogueInteraction.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Player) {
            isPlayerInRange = false;
            dialogueInteraction.SetActive(false);
        }
    }
}
