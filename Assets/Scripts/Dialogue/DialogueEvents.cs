using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    private DialogueManager dialogueManager;
    private List<GameObject> presentCharacters;

    void Awake() {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
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
        bool isFinal = false
        ) 
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
    public IEnumerator ConvoIntro1()
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
            LineBuilder("A phone starts ringing. \nWalk over to the phone, and press \'E\' to interact!", CharacterNames.Narrator, "", soundName:"PhoneLinging")
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoIntro2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie"),
            GameObject.Find("Phone_Guy")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Phone_Guy, expressionName = "phone_!" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Oh Carrie, my cute little apprentice! Thanks for all your hard work! Now, it's time for your next assignment.", CharacterNames.Phone_Guy, "???"),
            LineBuilder("W-what? But I just finished my last one—", CharacterNames.Carrie, "Carrie", expressionName:"worried"),
            LineBuilder("Alright! So! According to our logs, you've helped 727 people in purgatory ascend to heaven. ", CharacterNames.Phone_Guy, "???"),
            LineBuilder("What! I've helped that many people already? Wait, how long have I been here?", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("Don't worry about it. Ya know time doesn't really exist here, unlike Earth.", CharacterNames.Phone_Guy, "???", expressionName:"phone_tehet"),
            LineBuilder("Anyway! You're *this* close to becoming a full fledged angel. All you gotta do is help the next batch of people, and you'll earn your halo.", CharacterNames.Phone_Guy, "???", expressionName:"phone_talking"),
            LineBuilder("!!! Oh my Go", CharacterNames.Carrie, "Carrie", autoSkip:true, expressionName:"sparkle"),
            LineBuilder("Watch it.", CharacterNames.Phone_Guy, "???", expressionName:"phone_grr"),
            LineBuilder("... Er, sorry. I'm just super excited! I've been waiting for ages! Literally!", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("Now that's the spirit! I take it you'll do your absolute bestest for your final batch.", CharacterNames.Phone_Guy, "???", expressionName:"phone_heart"),
            LineBuilder("Good luck! You got this! And other words of encouragement! Yeah! Ok byeeeeeeeeeeeeeeeeeeeeee!", CharacterNames.Phone_Guy, "???", expressionName:"phone_!"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie", soundName:"BusySignal"),
            LineBuilder("I can't believe it! I'm finally gonna be a full fledged angel, and go to heaven!!", CharacterNames.Carrie, "Carrie", expressionName:"sparkle"),
            LineBuilder("The door closest to you lights up. \nInteract with in-world elements using \"E\" key!", CharacterNames.Narrator, "", soundName:"BellHit"),
            LineBuilder("Alright, Carrie... let's get to work!!", CharacterNames.Carrie, "Carrie", expressionName:"sparkle", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }

    public IEnumerator ConvoEnterBroRoom()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Gah... what is this smell...", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Wow... this is an... interesting room. No—I can't be judging others as an angel! Let's just smile! and... get to work.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I think I see someone sitting at that desk. Let's go talk to them.", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }

    public IEnumerator ConvoBrother1()
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
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Brother, expressionName = "freaky" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Oh yes... God, you're so cute, Asuna-chan. I'm... ", CharacterNames.Brother, "???"),
            LineBuilder("What the—! I'm sorry, what are you doing?!", CharacterNames.Carrie, "Carrie", actionName:"angryhop"),
            LineBuilder("Huh? Who are you?? And what are you doing in my room??", CharacterNames.Brother, "???", expressionName:"angry", actionName:"shake"),
            LineBuilder("Er, my name is Carrie! But more importantly, what the [REDACTED] were you doing on that computer, kid??", CharacterNames.Carrie, "Carrie"),
            LineBuilder("KID?! I ain't no kid, I'm 13! And the name's Austin, too!", CharacterNames.Brother, "Austin", actionName:"angryhop"),
            LineBuilder("What do you want, anyway? I'm busy here! I'm about to make Asuna-chan my...", CharacterNames.Brother, "Austin", expressionName:"tired"),
            LineBuilder("Don't need to hear that! I'm an angel and I'm here to help you! With... whatever it is you need to move on, I guess.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("<i>That being said, I don't know if I really wanna help this kid...</i>", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Huh? Help with what? You can't help me with anything. Just get out of my room! I want to play my games in PEACE, without the presence of a 3D woman.", CharacterNames.Brother, "Austin"),
            LineBuilder("<i>3D, huh...</i>", CharacterNames.Carrie, "Carrie"),
            LineBuilder("So, about that character you were talking about—", CharacterNames.Carrie, "Carrie"),
            LineBuilder("You mean, you mean Asuna-chan!", CharacterNames.Brother, "Austin", expressionName:"happy", actionName:"hop"),
            LineBuilder("Er... yeah. I don't know where she's from but I can tell she's from a game, based on the countless posters and figurines you have.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Carrie picks up the letter bracelet next to them.", CharacterNames.Narrator, ""),
            LineBuilder("What's this? It's like, the only different thing in this room. Is this yours?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I-I'm not sure. But I don't want you touching that.", CharacterNames.Brother, "Austin", expressionName:"embarassed"),
            LineBuilder("How does this make you feel? What can you remember?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("...", CharacterNames.Brother, "Austin", actionName:"shake"),
            LineBuilder("<i>Looks like I can use this...</i>", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Carrie reaches out her hand, and touches the crusty computer screen. Suddenly, a blinding light envelopes the room.", CharacterNames.Narrator, ""),
            LineBuilder("Wh...what's happening??", CharacterNames.Brother, "Austin", expressionName:"stupid", actionName:"shake"),
            LineBuilder("It's time, Austin. It's time to confront whatever it is that's holding you back.", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }

    public IEnumerator ConvoBrother2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Brother, expressionName = "unimpressed" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("I finally got you!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("No, no! Get off of me! Let me go!", CharacterNames.Brother, "Austin", actionName:"shake"),
            LineBuilder("Austin, look at this bracelet. Try to remember!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I-I...", CharacterNames.Brother, "Austin", actionName:"shake", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoBrother3()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" },
            new InitialExpression { character = CharacterNames.Brother, expressionName = "bashful" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("...I remember... my sister, she gave this to me. She said it was supposed to be a lucky charm, to keep me safe.", CharacterNames.Brother, "Austin"),
            LineBuilder("B-but... SHE should've been the one to have it, not me. Maybe then, she would've still been alive.", CharacterNames.Brother, "Austin", expressionName:"sad", actionName:"shake"),
            LineBuilder("Austin, don't think like that. Her death was unavoidable. Things happen. But she continues to live on in you, right?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("He clenches the bracelet in his hand extra tightly.", CharacterNames.Narrator, ""),
            LineBuilder("... Right... yeah, you're right. Maybe it's time I stopped running.", CharacterNames.Brother, "Austin", expressionName:"tired"),
            LineBuilder("That's it, Austin. Take your time with it. You've been holding onto this for so long—it's okay to let yourself feel it.", CharacterNames.Carrie, "Carrie", expressionName:"happy"),
            LineBuilder("She was the one who got me into all this. The games, the characters. She loved it all, you know?", CharacterNames.Brother, "Austin", expressionName:"normal"),
            LineBuilder("And now, I...", CharacterNames.Brother, "Austin", expressionName:"tired"),
            LineBuilder("...I keep playing, hoping, hoping it'll feel like she's still here playing with me, somehow.", CharacterNames.Brother, "Austin", expressionName:"sad"),
            LineBuilder("And in a way, she is. You're carrying her memory forward.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("She would've liked that. She would've wanted me to keep going, not get stuck here.", CharacterNames.Brother, "Austin", expressionName:"tired"),
            LineBuilder("He opens his eyes again, the pain in his face slowly giving way to a sense of peace.", CharacterNames.Narrator, ""),
            LineBuilder("Exactly. It's okay to love these worlds, to find escape, but don't let them be a cage. Let her memory inspire you to live, not to hide.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("He looks at the bracelet one last time before slipping it on his wrist.", CharacterNames.Narrator, ""),
            LineBuilder("Thanks, Carrie. I think... I think I can finally let it go.", CharacterNames.Brother, "Austin", expressionName:"normal"),
            LineBuilder("The room brightens, as if a weight has been lifted. The once-dim memories around them begin to shimmer, fading peacefully.", CharacterNames.Narrator, ""),
            LineBuilder("Anytime, Austin. Now, come on, it's time to go.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Haha... yeah. Let's go, Carrie.", CharacterNames.Brother, "Austin", expressionName:"happy", isFinal:true),

        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoBrother4()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" },
            new InitialExpression { character = CharacterNames.Brother, expressionName = "bashful" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Thanks, Carrie. For everything.", CharacterNames.Brother, "Austin", expressionName:"happy", isFinal:true),

        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    #endregion
}
