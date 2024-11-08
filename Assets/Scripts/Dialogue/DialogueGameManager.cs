using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.TextCore.Text;

public class DialogueGameManager : MonoBehaviour
{
    public List<DialogueCharacterManager> characters; // Reference to all characters on screen

    public List<int> answerPos;
    public GameObject answerObject;

    private Dictionary<CharacterNames, DialogueCharacterManager> characterMap;
    public GameObject dialogueText;
    public GameObject dialogueBox;
    public GameObject speakerBox;
    private DynamicPanel dynamicPanel;
    
    GameObject[] taggedSpeakers;
    private bool isDialogueActive = false;

    void Awake()
    {
        characterMap = new Dictionary<CharacterNames, DialogueCharacterManager>();
        dynamicPanel = speakerBox.GetComponent<DynamicPanel>();

        speakerBox.SetActive(false);
        dialogueBox.SetActive(false);

        taggedSpeakers = GameObject.FindGameObjectsWithTag("Speaker");
    }

    void Start()
    {

    }

    public void StartDialogueEvent(DialogueEvent dialogueEvent, List<GameObject> thisEventCharacters)
    {
        speakerBox.SetActive(true);
        dialogueBox.SetActive(true);
        foreach (var s in taggedSpeakers) {
            Debug.Log(s + " " + thisEventCharacters.Contains(s));
            if (thisEventCharacters.Contains(s)) {
                s.GetComponent<DialogueCharacterManager>().Invisible(false);
            } else {
                s.GetComponent<DialogueCharacterManager>().Invisible(true);
            }
        }

        characters.Clear();
        foreach (var c in thisEventCharacters) {
            DialogueCharacterManager dcManagerComponent = c.GetComponent<DialogueCharacterManager>();
            characters.Add(dcManagerComponent);
            
            if (!characterMap.ContainsKey(dcManagerComponent.characterName))
            {
                characterMap.Add(dcManagerComponent.characterName, dcManagerComponent);
            }

            if (dcManagerComponent.characterName == CharacterNames.Carrie)
            {
                dcManagerComponent.DoAction("fadein");
            }
        }

        // Initial Expressions
        foreach (var initExpr in dialogueEvent.initialExpressions)
        {
            foreach (var characterObj in thisEventCharacters)
            {
                DialogueCharacterManager characterManager = characterObj.GetComponent<DialogueCharacterManager>();
                if (characterManager.characterName == initExpr.character)
                {
                    characterManager.ChangeExpression(Resources.Load<Sprite>($"Expressions/{characterManager.characterName}/{initExpr.expressionName}"));
                    break;
                }
            }
        }

        Debug.Log("Starting dialogue event");
        StartCoroutine(DialogueRoutine(dialogueEvent));
    }

    IEnumerator DialogueRoutine(DialogueEvent dialogueEvent)
    {
        int currLineIndex = 0;
        while (currLineIndex < dialogueEvent.dialogueLines.Count) 
        {
            var line = dialogueEvent.dialogueLines[currLineIndex];
            DialogueCharacterManager character = GetCharacter(line.speaker);
            bool isNarrator = line.speaker == CharacterNames.Narrator;
            
            SetSpeaker(line.speaker);
            if (line.expressionName != "none")
            {
                string expressionPath = $"Expressions/{line.speaker}/{line.expressionName}";
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
            if (line.soundName != "none")
            {
                FindAnyObjectByType<AudioManager>().Play(line.soundName);
            }

            Debug.Log("Starting line");
            isDialogueActive = true;
            dynamicPanel.UpdateUISpeaker(line.speaker, line.speakerLabel, isNarrator);
            yield return StartCoroutine(dialogueText.GetComponent<DialogueTranslator>().TypeDialogue(line.text, line.speaker, line.autoSkip));
            
            isDialogueActive = false;
            if (line.autoSkip)
            {
                currLineIndex++;
                continue;
            }
            yield return new WaitForSeconds(0.5f);

            if (line.hasAnswer)
            {
                yield return StartCoroutine(ShowDialogueOptions(line.answers));
                // currLineIndex = GetNextLineIndex(line, selectedOptionIndex);
            }
            else
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0));
            }

            if (line.isFinal)
            {
                break;
            } else {
                currLineIndex += line.skipLines > 0 ? line.skipLines : 1;
            }
        }
        dialogueBox.SetActive(false);
        speakerBox.SetActive(false);
        foreach (var s in taggedSpeakers) {
            s.GetComponent<DialogueCharacterManager>().Invisible(true);
        }
        CutsceneStarter.inEvent = false;
        CutsceneStarter.cutsceneNum++;
    }

    IEnumerator ShowDialogueOptions(List<DialogueOption> options)
    {
        foreach (int i in answerPos)
        {
            Instantiate(answerObject, new Vector3(i, -127, 0), new Quaternion(0,0,0,0)) ;
        }
        yield return null;
        //UIManager.ShowOptions(options);

        //bool optionSelected = false;
        //int selectedOptionIndex = -1;

        //UIManager.OnOptionSelected += (index) =>
        //{
        //    optionSelected = true;
        //    selectedOptionIndex = index;
        //};
        //yield return new WaitUntil(() => optionSelected);

        //UIManager.HideOptions();
        //// Log the player's choice and determine the next line.
        //int nextLineIndex = options[selectedOptionIndex].nextLineIndex;
        //currentLineIndex = nextLineIndex;
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
