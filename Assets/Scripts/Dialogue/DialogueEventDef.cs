using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public CharacterNames speaker;
    public string speakerLabel;
    public string text;
    public bool hasAnswer;
    public string expressionName;   // Name of the expression sprite to change to (optional)
    public string actionName;   // Same thing but action
}

[System.Serializable]
public class DialogueEvent
{
    public List<DialogueLine> dialogueLines;    // Not necessarily one sentence—just a "section" of speak
}
