using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    private DialogueGameManager dialogueManager;
    private List<GameObject> presentCharacters;

    void Awake() {
        dialogueManager = FindFirstObjectByType<DialogueGameManager>();
    }

    private DialogueLine LineBuilder(string text, CharacterNames speaker, string speakerLabel, bool hasAnswer = false, string expressionName = "none", string actionName = "none") 
    {
        return new DialogueLine 
        { 
            speaker = speaker, 
            speakerLabel = speakerLabel, 
            text = text, 
            hasAnswer = hasAnswer,
            expressionName = expressionName, 
            actionName = actionName
        };
    }

    // Below are all of the Dialogue Events in the Game
    public void ConvoBrother_1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Monday 08:11 \nBrother's Room", CharacterNames.Narrator, "   "),
            LineBuilder("Oh yes... God, you're so cute, Asuna-chan. I'm so close... ", CharacterNames.Brother, "Brother"),
            LineBuilder("What the hell is wrong with you!", CharacterNames.Carrie, "???"),
            LineBuilder("I'm Carrie, and it looks like you need some correction!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("What do you think?", CharacterNames.Carrie, "Carrie", true),
            LineBuilder("Little bro was cooked.", CharacterNames.Narrator, "   ")
        };
        dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters);
    }

}
