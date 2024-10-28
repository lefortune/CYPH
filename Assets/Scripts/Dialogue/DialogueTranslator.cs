using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTranslator : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.1f;

    private void Awake()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
        dialogueText.text = "";
    }

    public IEnumerator TypeDialogue(string dialogue, CharacterNames speaker)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            if (letter != ' ') {
                if (speaker == CharacterNames.Narrator) {
                    FindObjectOfType<AudioManager>().Play("DialogueTyping");
                }
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
