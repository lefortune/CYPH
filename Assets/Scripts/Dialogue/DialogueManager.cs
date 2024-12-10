using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public List<SpeakerManager> speakerManagerList; // Reference to all characters on screen

    public List<int> answerPos;
    public GameObject answerObject;
    public bool answerSelected;

    private Dictionary<CharacterNames, SpeakerManager> characterMap;
    public GameObject dialogueText;
    public GameObject dialogueBox;
    public GameObject speakerBox;
    private DynamicPanel dynamicPanel;
    
    GameObject[] taggedSpeakers;
    public static bool isDialogueActive = false;
    private bool instaskip = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        characterMap = new Dictionary<CharacterNames, SpeakerManager>();
        dynamicPanel = speakerBox.GetComponent<DynamicPanel>();

        speakerBox.SetActive(false);
        dialogueBox.SetActive(false);

        taggedSpeakers = GameObject.FindGameObjectsWithTag("Speaker");
    }

    void Update()
    {
        if (dialogueText == null) {
            dialogueText = GameObject.Find("DialogueText");
        }
        if (dialogueBox == null) {
            dialogueBox = GameObject.Find("DialogueBox");
        }
        if (speakerBox == null) {
            speakerBox = GameObject.Find("SpeakerBox");
        }

        if (Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.C)) {
            instaskip = true;
        }
    }

    public IEnumerator StartDialogueEvent(DialogueEvent dialogueEvent, List<GameObject> thisEventCharacters)
    {
        speakerBox.SetActive(true);
        dialogueBox.SetActive(true);
        foreach (var s in taggedSpeakers) {
            Debug.Log(s + " " + thisEventCharacters.Contains(s));
            if (thisEventCharacters.Contains(s)) {
                s.GetComponent<SpeakerManager>().FadeIn();
            }
        }

        speakerManagerList.Clear();
        foreach (var c in thisEventCharacters) {
            SpeakerManager speakerManagerComponent = c.GetComponent<SpeakerManager>();
            speakerManagerList.Add(speakerManagerComponent);
            
            if (!characterMap.ContainsKey(speakerManagerComponent.characterName))
            {
                characterMap.Add(speakerManagerComponent.characterName, speakerManagerComponent);
            }
        }

        // Initial Expressions
        foreach (var initExpr in dialogueEvent.initialExpressions)
        {
            foreach (var characterObj in thisEventCharacters)
            {
                SpeakerManager characterManager = characterObj.GetComponent<SpeakerManager>();
                if (characterManager.characterName == initExpr.character)
                {
                    characterManager.ChangeExpression(Resources.Load<Sprite>($"Expressions/{characterManager.characterName}/{initExpr.expressionName}"));
                    break;
                }
            }
        }

        Debug.Log("Starting dialogue event");
        yield return StartCoroutine(DialogueRoutine(dialogueEvent));

        dialogueBox.SetActive(false);
        speakerBox.SetActive(false);
        foreach (var s in taggedSpeakers) {
            s.GetComponent<SpeakerManager>().Hide();
        }
        CutscenesManager.inEvent = false;
        Debug.Log("Finished dialogue event");
    }

    IEnumerator DialogueRoutine(DialogueEvent dialogueEvent)
    {
        int currLineIndex = 0;
        while (currLineIndex < dialogueEvent.dialogueLines.Count) 
        {
            var line = dialogueEvent.dialogueLines[currLineIndex];
            SpeakerManager character = GetCharacter(line.speaker);
            bool isNarrator = line.speaker == CharacterNames.Narrator;
            
            foreach (var speaker in speakerManagerList)
            {
                if (speaker.characterName == line.speaker)
                    speaker.Focus();
                else
                    speaker.Unfocus();
            }

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

            Debug.Log(line.speaker + ", isNarrator = " + isNarrator);

            dynamicPanel.UpdateUISpeaker(line.speaker, line.speakerLabel, isNarrator);
            yield return StartCoroutine(dialogueText.GetComponent<DialogueTranslator>().TypeDialogue(line.text, line.speaker, line.autoSkip));
            
            isDialogueActive = false;
            if (line.autoSkip)
            {
                currLineIndex++;
                continue;
            }
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0));

            if (instaskip) 
            {
                instaskip = false;
                break;
            }

            if (line.isFinal)
            {
                break;
            } else {
                currLineIndex += line.skipLines > 0 ? line.skipLines : 1;
            }
        }
    }

    public SpeakerManager GetCharacter(CharacterNames speaker)
    {
        if (characterMap.TryGetValue(speaker, out SpeakerManager character))
        {
            return character;
        }
        return null;
    }
        
}
