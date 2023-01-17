using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour{
    
    int lineIndex;

    bool isPlayerInRange;
    bool didDialogueStart;

    [SerializeField] float tipingTime = 0.05f;
    [SerializeField, TextArea(4,6)] string[] dialogueLines;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject dialogueMark;
    [SerializeField] TMP_Text dialogueText;

    void Update() {
        
        if(isPlayerInRange == true && Input.GetKeyDown(KeyCode.E)){

            if(!didDialogueStart){

                StartDialogue();
            }else if(dialogueText.text == dialogueLines[lineIndex]){

                NextDialogueLine();
            }else{

                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    void StartDialogue(){

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    void NextDialogueLine(){

        lineIndex++;
        if(lineIndex < dialogueLines.Length){

            StartCoroutine(ShowLine());
        }else{

            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1;
        }
    }

    IEnumerator ShowLine(){

        dialogueText.text = string.Empty;

        foreach(char c in dialogueLines[lineIndex]){

            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(tipingTime);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        
        if(coll.gameObject.name == "Player"){

            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        
        if(coll.gameObject.name == "Player"){

            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}