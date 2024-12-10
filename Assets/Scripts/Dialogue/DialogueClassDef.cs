using System.Collections.Generic;

// Dialogue Events — Main "visual novel-esque" interactions between Carrie and important characters.
#region Dialogue Events
[System.Serializable]
public class DialogueLine
{
    public CharacterNames speaker;
    public string speakerLabel; // What will be shown in speaker box
    public string text;
    public bool autoSkip;   // If the dialogue automatically continues, without clicking
    public int skipLines;   // For dialog choices with unique dialog, the last line should point to a different index (RELATIVE)
    public string expressionName;   // Name of the expression sprite to change to ("none" for nochange)
    public string actionName;   // Same thing but action
    public string soundName;
    public bool isFinal;    // If the dialogue ends following this line
}
public class DialogueOption 
{
    public string optionText;
    public int nextLineIndex; // Index of the next line in the dialogue list based on this choice.
}
[System.Serializable]
public class InitialExpression
{
    public CharacterNames character;
    public string expressionName;
}
[System.Serializable]
public class DialogueEvent
{
    public List<DialogueLine> dialogueLines;    // Not necessarily one sentence—just a "section" of speak
    public List<InitialExpression> initialExpressions;
}
#endregion

// Background Interactables — Avatarless interactions with the environment.
#region Background Interactable Text
[System.Serializable]
public class InteractableTextLine
{
    public string text;
    public bool isFinal;
}

[System.Serializable]
public class InteractableTexts
{
    public List<InteractableTextLine> interactableLines;
}
#endregion

// Cutscene events — Dialogue events that take place within the same scene, notably for intro scene.
#region Cutscene Events
[System.Serializable]
public class CutsceneLine
{
    public CharacterNames speaker;
    public string speakerLabel;
    public string text;
    public string expressionName;
    public string actionName;
    public bool isFinal; 
}

[System.Serializable]
public class CutsceneEvent
{
    public List<CutsceneLine> cutsceneLines;
}
#endregion