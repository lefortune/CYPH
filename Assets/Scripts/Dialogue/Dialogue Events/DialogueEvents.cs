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
        bool autoSkip = false,
        bool hasAnswer = false, 
        List<DialogueOption> answers = null, 
        int skipLines = 0,
        string expressionName = "none", 
        string actionName = "none",
        string soundName = "none",
        bool isFinal = false) 
    {
        return new DialogueLine 
        { 
            text = text, 
            speaker = speaker, 
            speakerLabel = speakerLabel, 
            autoSkip = autoSkip,
            hasAnswer = hasAnswer,
            answers = answers,
            skipLines = skipLines,
            expressionName = expressionName, 
            actionName = actionName,
            soundName = soundName,
            isFinal = isFinal
        };
    }

    // Below are all of the Dialogue Events in the Game
    #region Dialogue events
    public void IntroCutscenePt1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "happy" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Phew! I think that wraps up my clients for today!", CharacterNames.Carrie, "???"),
            LineBuilder("Finally, I can sit back and relax a litt—", CharacterNames.Carrie, "???"),
            LineBuilder("A phone starts ringing. \nWalk over to the phone, and press \'F\' to interact!", CharacterNames.Narrator, "", soundName:"PhoneLinging")
        };
        dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters);
    }
    public void IntroCutscenePt2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie"),
            GameObject.Find("Phone_Guy")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Oh Carrie, my cute little apprentice! Thanks for all your hard work! Now, it's time for your next assignment.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("W-what? But I just finished my last one—", CharacterNames.Carrie, "Carrie", expressionName:"worried"),
            LineBuilder("Alright! So! According to our logs, you've helped 727 people in purgatory ascend to heaven. ", CharacterNames.Phone_Guy, "???"),
            LineBuilder("What! I've helped that many people already? Wait, how long have I been here?", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("Don't worry about it. Ya know time doesn't really exist here, unlike Earth.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("Anyway! You're *this* close to becoming a full fledged angel. All you gotta do is help the next batch of people, and you'll earn your halo.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("!!! Oh my Go", CharacterNames.Carrie, "Carrie", autoSkip:true, expressionName:"sparkle"),
            LineBuilder("Watch it.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("... Er, sorry. I'm just super excited! I've been waiting for ages! Literally!", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("Now that's the spirit! I take it you'll do your absolute bestest for your final batch.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("Good luck! You got this! And other words of encouragement! Yeah! Ok byeeeeeeeeeeeeeeeeeeeeee!", CharacterNames.Phone_Guy, "???"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie", soundName:"BusySignal"),
            LineBuilder("I can't believe it! I'm finally gonna be a full fledged angel, and go to heaven!!", CharacterNames.Carrie, "Carrie", expressionName:"sparkle"),
        };
        dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters);
    }
    public void IntroCutscenePt3()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "sparkle" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("There it is!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Alright, Carrie... let's get to work!!", CharacterNames.Carrie, "Carrie", isFinal:true)
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
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" }
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
