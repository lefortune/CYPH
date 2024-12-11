using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTranslator : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.03f;
    float skipDelay = 0.15f;
    bool skipTyping;

    private void Awake()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
        dialogueText.text = "";
    }

    public IEnumerator TypeDialogue(string dialogue, CharacterNames speaker = CharacterNames.Narrator, bool autoSkip = false)
    {
        dialogueText.text = "";
        skipTyping = false;
        float skipTimer = 0f;

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            if (letter != ' ') {
                if (speaker == CharacterNames.Narrator) 
                {
                    FindObjectOfType<AudioManager>().Play("DialogueTyping");
                }
                if (speaker == CharacterNames.Seraph) 
                {
                    FindObjectOfType<AudioManager>().PlayOneShot("SeraphDialogueTyping");
                }
            }

            float elapsedTime = 0f;
            while (elapsedTime < typingSpeed)
            {
                skipTimer += Time.deltaTime;
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && skipTimer >= skipDelay && !autoSkip)
                {
                    skipTyping = true;
                    break;
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (skipTyping)
            {
                break;
            }
        }

        if (skipTyping)
        {
            dialogueText.text = dialogue; // Complete dialogue instantly
        }
    }
}
