using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{
    public List<string> dialogueOptions;
    public string dialogueOption1;
    public string dialogueOption2;
    public string dialogueOption3;
    public string dialogueOption4;
    public string dialogueOption5;
    public TMP_Text currentText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueOptions[0] = dialogueOption1;
        dialogueOptions[1] = dialogueOption2;
        dialogueOptions[2] = dialogueOption3;
        dialogueOptions[3] = dialogueOption4;
        dialogueOptions[4] = dialogueOption5;
        currentText = GetComponent<TMP_Text>();
    }

    public void NextDialogue(int dialogueIndex)
    {
        currentText.text = dialogueOptions[dialogueIndex];
    }
}
