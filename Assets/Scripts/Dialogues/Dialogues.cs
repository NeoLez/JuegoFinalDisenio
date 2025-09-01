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
    [SerializeField, TextArea(3, 5)] private string[] dialogueLines;

    private void Awake() {
        GameManager.Input.WorldInteractions.Interact.performed += PlayerInteracted;
    }

    private void PlayerInteracted(InputAction.CallbackContext ctx) {
        if (isDialoguePlaying) {
            if (isFillingInLine) {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                isFillingInLine = false;
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
            GameManager.Input.Pause.Disable();
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
            GameManager.Input.Pause.Enable();
        }
    }
    private void EndDialogue()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        if (shouldStartAuto) 
        {
            hasAutoPlayed = true;
        }else {
            dialogueInteraction.SetActive(true);
        }
    }
    private IEnumerator ShowLine() {
        isFillingInLine = true;
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }

        isFillingInLine = false;
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
