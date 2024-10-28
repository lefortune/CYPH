using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.TextCore.Text;

public class DialogueGameManager : MonoBehaviour
{
    public List<DialogueCharacterManager> characters; // Reference to all characters on screen

    private Dictionary<CharacterNames, DialogueCharacterManager> characterMap;
    public GameObject dialogueText;
    public GameObject speakerBox;
    private DynamicPanel dynamicPanel;
    private bool isDialogueActive = false;

    void Awake()
    {
        characterMap = new Dictionary<CharacterNames, DialogueCharacterManager>();
        dynamicPanel = speakerBox.GetComponent<DynamicPanel>();
    }

    public void StartDialogueEvent(DialogueEvent dialogueEvent, List<GameObject> thisEventCharacters)
    {
        foreach (var c in thisEventCharacters) {
            DialogueCharacterManager dcManagerComponent = c.GetComponent<DialogueCharacterManager>();
            characters.Add(dcManagerComponent);
            if (!characterMap.ContainsKey(dcManagerComponent.characterName))
            {
                Debug.Log("No duplicate character name, adding to map");
                characterMap.Add(dcManagerComponent.characterName, dcManagerComponent);
            }
            else
            {
                Debug.LogWarning($"Duplicate character name detected: {dcManagerComponent.characterName}");
            }
        }
        Debug.Log("Starting dialogue event");
        StartCoroutine(DialogueRoutine(dialogueEvent));
    }

    IEnumerator DialogueRoutine(DialogueEvent dialogueEvent)
    {
        foreach (var line in dialogueEvent.dialogueLines) {
            DialogueCharacterManager character = GetCharacter(line.speaker);
            
            SetSpeaker(line.speaker);
            if (line.expressionName != "none")
            {
                string expressionPath = $"Expressions/{line.expressionName}";
                Sprite newExpression = Resources.Load<Sprite>(expressionPath);
                if (newExpression != null) {
                    character.ChangeExpression(newExpression);
                }
                else {
                    Debug.LogWarning($"Could not find expression sprite at path: {expressionPath}");
                }
            }
            if (line.actionName != "none")
            {
                character.DoAction(line.actionName);
            }

            Debug.Log("Starting line");
            isDialogueActive = true;
            dynamicPanel.UpdateUISpeaker(line.speaker, line.speakerLabel);
            yield return StartCoroutine(dialogueText.GetComponent<DialogueTranslator>().TypeDialogue(line.text, line.speaker));
            isDialogueActive = false;
            MakeAnswerOptions();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
    }

    void MakeAnswerOptions()
    {
        if (Input.GetMouseButtonDown(0)) {

        }
    }

    public void SetSpeaker(CharacterNames speaker)
    {
        foreach (var character in characters) {
            if (speaker == character.characterName)
            {
                character.SetFocused();
            }
            else
            {
                character.SetUnfocused();
            }
        }
    }

    public DialogueCharacterManager GetCharacter(CharacterNames speaker)
    {
        if (characterMap.TryGetValue(speaker, out DialogueCharacterManager character))
        {
            return character;
        }
        return null;
    }
    
}
