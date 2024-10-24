using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTranslator : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;

    private void Start()
    {
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(dialogueText.text);
        //dialogueText.text = ""; // Clear text at start
    }

    public void StartDialogue(string dialogue)
    {
        StartCoroutine(TypeDialogue(dialogue));
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
