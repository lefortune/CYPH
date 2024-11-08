using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DialogueEvents : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    private DialogueGameManager dialogueManager;
    private List<GameObject> presentCharacters;

    void Awake() {
        dialogueManager = FindFirstObjectByType<DialogueGameManager>();
    }

    private DialogueLine LineBuilder(
        string text, 
        CharacterNames speaker, 
        string speakerLabel, 
        bool hasAnswer = false, 
        List<DialogueOption> answers = null, 
        int skipLines = 0,
        string expressionName = "none", 
        string actionName = "none",
        bool isFinal = false) 
    {
        return new DialogueLine 
        { 
            text = text, 
            speaker = speaker, 
            speakerLabel = speakerLabel, 
            hasAnswer = hasAnswer,
            answers = answers,
            skipLines = skipLines,
            expressionName = expressionName, 
            actionName = actionName,
            isFinal = isFinal
        };
    }

    // Below are all of the Dialogue Events in the Game
    #region Dialogue events
    public void IntroCutscene()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie"),
            GameObject.Find("Phone Guy")
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Phew!", CharacterNames.Carrie, "???", actionName:"angryhop"),
            LineBuilder("My name is Carrie!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("...", CharacterNames.Brother, "Brother", actionName:"shake"),
            LineBuilder("Little bro was cooked.", CharacterNames.Narrator, "", isFinal:true)
        };
        dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters);
    }

    public void ConvoBrother_1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        List<DialogueOption> options = new List<DialogueOption>
        {
            new DialogueOption { optionText = "Yes", nextLineIndex = 5 },
            new DialogueOption { optionText = "No", nextLineIndex = 6 },
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Monday 08:11 \nBrother's Room", CharacterNames.Narrator, ""),
            LineBuilder("Oh yes... God, you're so cute, Asuna-chan. I'm so close... ", CharacterNames.Brother, "Brother"),
            LineBuilder("What the hell is wrong with you!", CharacterNames.Carrie, "???", actionName:"angryhop"),
            LineBuilder("My name is Carrie!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("And it looks like you need some correction!", CharacterNames.Carrie, "Carrie", true, options),
            LineBuilder("...", CharacterNames.Brother, "Brother", actionName:"shake", hasAnswer:true, answers:options),
            LineBuilder("Little bro was cooked.", CharacterNames.Narrator, "", isFinal:true)
        };
        dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters);
    }
    #endregion
}
