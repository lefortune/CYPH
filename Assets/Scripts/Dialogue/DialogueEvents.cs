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
            skipLines = skipLines,
            expressionName = expressionName, 
            actionName = actionName,
            soundName = soundName,
            isFinal = isFinal
        };
    }

    // Below are all of the Dialogue Events in the Game
    #region Generic events
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
            LineBuilder("A phone starts ringing. \nWalk over to the phone, and press \'E\' to interact!", CharacterNames.Narrator, "", soundName:"PhoneLinging", isFinal:true)
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
    #endregion

    #region Brother Events
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
    public IEnumerator ConvoBrotherEnd()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "happy" },
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Whew, that sure was some much needed cardio! I hope he finds peace in heaven.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Now, time for the next room!", CharacterNames.Carrie, "Carrie", expressionName:"sparkle"),
            LineBuilder("Wait, what's that on that floor? It seems to be a torn up part of a photo?", CharacterNames.Carrie, "Carrie", expressionName:"surprised", isFinal:true),

        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    #endregion

    #region Mother events
    public IEnumerator IntroMother()
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
            LineBuilder("Alright! Now that I'm out of that place.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Looks like my next client is gonna be in the next door to the right!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I wonder who it's gonna be this time~", CharacterNames.Carrie, "Carrie", expressionName:"sparkle", isFinal:true)
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoEnterMomRoom()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Oh! This kitchen’s super spotless! But so… empty? ", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Maybe I should talk to that lady over there.", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoMother1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Mother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Mother, expressionName = "yeowch" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Spotless... Everything needs to be clean... ", CharacterNames.Mother, "???"),
            LineBuilder("Uh, hello there!", CharacterNames.Carrie, "Carrie", actionName:"hop"),
            LineBuilder("AHH! Excuse me?! What are you doing in my kitchen?!", CharacterNames.Mother, "???", expressionName:"angry", actionName:"shake"),
            LineBuilder("I-I'm sorry! I didn't mean to intrude but I'm an angel named—", CharacterNames.Carrie, "Carrie"),
            LineBuilder("An angel?! Well, aren't I blessed? First, my kitchen gets invaded, and now angels are dropping by uninvited. What's next? A choir in my living room?", CharacterNames.Mother, "???", actionName:"angryhop"),
            LineBuilder("The woman turns away, furiously scrubbing the countertop, muttering to herself.", CharacterNames.Narrator, ""),
            LineBuilder("Wait, I'm not here to bother you—I just noticed how perfectly clean this place is. You're doing an amazing job!", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Amazing?", CharacterNames.Mother, "???", expressionName:"surprised"),
            LineBuilder("Amazing doesn't keep the chaos out. Amazing doesn't stop the dust from settling back in.", CharacterNames.Mother, "???", expressionName:"yeowch"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie", expressionName:"worried"),
            LineBuilder("Chaos? Are you talking about the kitchen, or something else?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("It's all the same!", CharacterNames.Mother, "???", expressionName:"angry", actionName:"hop"),
            LineBuilder("Dust, mess, stains… They always come back. You can't stop them. You just... have to keep cleaning.", CharacterNames.Mother, "???", expressionName:"yeowch"),
            LineBuilder("Carrie inches closer to the woman, her curiosity growing.", CharacterNames.Narrator, ""),
            LineBuilder("I can see you've been working hard, but maybe you could take a break? Even angels take breaks sometimes.", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("Kahaha. A break? Now that's a luxury I can't afford. There's always more to do. Now, if you're done, I have work to do.", CharacterNames.Mother, "???", expressionName:"bitterlaugh"),
            LineBuilder("Wait, I didn't catch your name. My name is Carrie.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("...it's Linda.", CharacterNames.Mother, "Linda", expressionName:"yeowch"),
            LineBuilder("Carrie stands there for a moment, unsure of what to say. Her eyes wander to a cupboard left slightly ajar.", CharacterNames.Narrator, ""),
            LineBuilder("Inside, she notices a brightly pink cup that stands out from the otherwise muted colors of the kitchenware.", CharacterNames.Narrator, ""),
            LineBuilder("That's a cute cup!", CharacterNames.Carrie, "Carrie", expressionName:"happy"),
            LineBuilder("DON'T TOUCH THAT!", CharacterNames.Mother, "Linda", expressionName:"angry", actionName:"hop"),
            LineBuilder("I-I wasn't going to break it, I swear!", CharacterNames.Carrie, "Carrie", expressionName:"worried", actionName:"shake"),
            LineBuilder("It doesn't matter. Don't. Touch. Anything.", CharacterNames.Mother, "Linda"),
            LineBuilder("Linda slams the cupboard shut, her breathing heavy. Carrie steps back, sensing she's hit a nerve.", CharacterNames.Narrator, ""),
            LineBuilder("I'm sorry. I didn't mean to upset yo—", CharacterNames.Carrie, "Carrie", autoSkip:true),
            LineBuilder("Just. Just go. Please.", CharacterNames.Mother, "Linda", expressionName:"yeowch"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie"),
            LineBuilder("<i>Maybe... The cup could be the key to getting Linda out. If only I can get that cup without getting caught!</i>", CharacterNames.Carrie, "Carrie", expressionName:"oo", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoMother2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Mother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "happy" },
            new InitialExpression { character = CharacterNames.Mother, expressionName = "yeowch" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Phew! Finally got the cup! <i>It's so cute!</i>", CharacterNames.Carrie, "Carrie"),
            LineBuilder("AHH! I told you not to touch ANYTHING!", CharacterNames.Mother, "Linda", expressionName:"angry", actionName:"hop"),
            LineBuilder("She rushes toward Carrie, but stops abruptly when she sees the cup in Carrie's hands. Her face twists with a mix of anger and pain.", CharacterNames.Narrator, ""),
            LineBuilder("...put it back. Please... just put it back.", CharacterNames.Mother, "Linda", expressionName:"yeowch"),
            LineBuilder("I'm sorry, Linda, but I think this cup is important. I don't want to hurt you, but I feel like you need to remember why you've kept it.", CharacterNames.Carrie, "Carrie", expressionName:"worried"),
            LineBuilder("Linda clenches her fists, her breathing shaky. Tears well in her eyes as she stares at the cup.", CharacterNames.Narrator, ""),
            LineBuilder("It... it was... hers.", CharacterNames.Mother, "Linda", expressionName:"holding"),
            LineBuilder("Your daughter's?", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("She gave it to me for my birthday. She saved up her allowance, said she wanted to buy me something \"just for fun.\"", CharacterNames.Mother, "Linda", expressionName:"cryingclosed"),
            LineBuilder("Kahaha... I told her I didn't need it, that it was too bright and silly for a serious kitchen like this.", CharacterNames.Mother, "Linda", expressionName:"crying"),
            LineBuilder("But she insisted. She said... she said I needed something to make me smile while I was working.", CharacterNames.Mother, "Linda"),
            LineBuilder("And now… she's gone. And all I have left is this stupid, bright cup.", CharacterNames.Mother, "Linda", expressionName:"cryingclosed", actionName:"shake"),
            LineBuilder("Carrie places the cup on the counter gently and steps closer to Linda.", CharacterNames.Narrator, ""),
            LineBuilder("She gave it to you because she loved you, Linda. Not because of how clean or serious your kitchen was, but because she wanted you to be happy.", CharacterNames.Carrie, "Carrie", expressionName:"normal"),
            LineBuilder("I fought with her the day she died. Because of our argument, she left home. I don't even remember what it was.", CharacterNames.Mother, "Linda"),
            LineBuilder("Now, I clean, clean to forget... But no amount of cleaning could fix it. No amount of anything could.", CharacterNames.Mother, "Linda"),
            LineBuilder("Linda, it's not your fault. Don't blame yourself anymore. She gave you that cup to remind you of her love.", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("Maybe it's time to let go of the guilt and remember her for the joy she brought you.", CharacterNames.Carrie, "Carrie", expressionName:"happy"),
            LineBuilder("Linda looks at Carrie, her face softening. For the first time, the obsessive tension in her movements seems to fade.", CharacterNames.Narrator, ""),
            LineBuilder("Let go...?", CharacterNames.Mother, "Linda", expressionName:"crying"),
            LineBuilder("Yes! She wouldn't want you to spend forever trapped here, cleaning. She'd want you to be free, to find peace.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Linda looks back at the cup, then slowly picks it up. She cradles it in her hands, her tears falling freely now.", CharacterNames.Narrator, ""),
            LineBuilder("My little girl. I'm so sorry.", CharacterNames.Mother, "Linda", expressionName:"cryingclosed"),
            LineBuilder("As Linda speaks, a soft, glowing light begins to fill the room. The kitchen feels warmer, less sterile. Linda looks around, startled, but not afraid.", CharacterNames.Narrator, ""),            
            LineBuilder("...what's happening?", CharacterNames.Mother, "Linda", expressionName:"thinking"),
            LineBuilder("The door to heaven is opening for you, Linda. It's time for you to leave this kitchen.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("...", CharacterNames.Mother, "Linda", expressionName:"holding"),
            LineBuilder("Thank you, Carrie. I think... I think I'm ready now.", CharacterNames.Mother, "Linda"),
            LineBuilder("Linda's form begins to fade into the light, her expression peaceful for the first time. The pink cup glows brightly in her hands before dissolving into light with her.", CharacterNames.Narrator, ""),            
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoMotherEnd()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "normal" },
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("...", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I really hope Linda will be happy.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Look, on the floor, another torn up part of a photo!", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("Guess she missed a spot, haha. Now, onto the next room!", CharacterNames.Carrie, "Carrie", expressionName:"happy", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    #endregion

    #region Father events
    public IEnumerator ConvoEnterDadBefore()
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
            LineBuilder("The gem on this door, it's... red?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I have a bad feeling about this... but, I have to push on!", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoEnterDadRoom()
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
            LineBuilder("Wh—what is this.... Am I... Is this even a room? This room's scary... Not what I signed up for at all!", CharacterNames.Carrie, "Carrie"),
            LineBuilder(" There's a guy there…. Should I go talk to him?", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFather1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Father"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" },
            new InitialExpression { character = CharacterNames.Father, expressionName = "normal" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Hello? Are you okay?", CharacterNames.Carrie, "Carrie", actionName:"hop"),
            LineBuilder("What do you want.", CharacterNames.Father, "???"),
            LineBuilder("I'm an angel named Carrie! I'm here to help you.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("The man scoffs, taking a swig from the bottle and slams it down on the table.", CharacterNames.Narrator, ""),
            LineBuilder("Help? That's rich. You angels just swoop in, acting like you can fix what can't be fixed.", CharacterNames.Father, "???"),
            LineBuilder("I can't fix everything, but I can try to help you move on. What's your name?", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("It's Nathan. Names don't matter anyway.", CharacterNames.Father, "Nathan"),
            LineBuilder("Well, nice to meet you, Nathan. I'm here to help you move on from purgatory to heaven.", CharacterNames.Carrie, "Carrie", expressionName:"happy"),
            LineBuilder("Me? Go to Heaven? PWAHAHA! That's the funniest thing I've heard in a long time. ", CharacterNames.Father, "Nathan", expressionName:"cynical"),
            LineBuilder("What? Why?", CharacterNames.Carrie, "Carrie", expressionName:"worried"),
            LineBuilder("I'm a sinner, girlie. That's why.", CharacterNames.Father, "Nathan", expressionName:"normal"),
            LineBuilder("A sinner? Why do you say that?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("None of your business! Just leave me alone, kid!", CharacterNames.Father, "Nathan", expressionName:"angry", actionName:"shake"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie"),
            LineBuilder("If you're here, you didn't do anything unforgivable. So please, just let me help you.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("You don't get it. I don't deserve forgiveness. I need to feel this. To repent. Alone.", CharacterNames.Father, "Nathan", expressionName:"normal"),
            LineBuilder("Nathan, no one deserves to suffer like this. Let me help—", CharacterNames.Carrie, "Carrie"),
            LineBuilder("I said no! I don't need your help!", CharacterNames.Father, "Nathan", expressionName:"angry", actionName:"shake"),
            LineBuilder("He stands suddenly, knocking over a stack of bottles. His anger is palpable as he glares at Carrie.", CharacterNames.Narrator, ""),
            LineBuilder("If you won't leave me alone...", CharacterNames.Father, "Nathan", expressionName:"cynical"),
            LineBuilder("Nathan grabs an empty bottle and hurls it at Carrie, narrowly missing her.", CharacterNames.Narrator, ""),
            LineBuilder("...then I'll teach you how to leave!", CharacterNames.Father, "Nathan", expressionName:"angry", actionName:"shake", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFather2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Father"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "normal" },
            new InitialExpression { character = CharacterNames.Father, expressionName = "angry" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Carrie wraps her arms around him in a firm but gentle hug. \nHe freezes from the unexpected act of kindness, then tries to push her away.", CharacterNames.Narrator, ""),
            LineBuilder("Get off me! You don't know what I've done!", CharacterNames.Father, "Nathan"),
            LineBuilder("I don't need to know everything, Nathan. I just know you're hurting, and you don't have to do this alone.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Nathan struggles for a moment but then stops, his body trembling. His voice cracks as he speaks.", CharacterNames.Narrator, "", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFather3()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Father"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Father, expressionName = "cynical" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("(choking) I was too busy drinking... too drunk to see my own daughter.", CharacterNames.Father, "Nathan"),
            LineBuilder("Carrie's grip loosens slightly, giving Nathan space to speak as his emotions spill out.", CharacterNames.Narrator, ""),
            LineBuilder("Because of that... because of me... she died.", CharacterNames.Father, "Nathan"),
            LineBuilder(".....", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("If... if I hadn't been drinking that night, I could've saved her. I could've been there.", CharacterNames.Father, "Nathan"),
            LineBuilder("But I wasn't. I didn't. And now she's gone, and it's all my fault!", CharacterNames.Father, "Nathan", expressionName:"sad", actionName:"shake"),
            LineBuilder("Nathan collapses to his knees, burying his face in his hands. Carrie kneels beside him, placing a hand gently on his shoulder.", CharacterNames.Narrator, ""),
            LineBuilder("Nathan, you didn't want this to happen. You're human, and humans make mistakes.", CharacterNames.Carrie, "Carrie", expressionName:"normal"),
            LineBuilder("No. This wasn't a mistake. It was a choice. I chose the bottle over her, and now she's gone.", CharacterNames.Father, "Nathan", actionName:"shake"),
            LineBuilder("You made a terrible choice, yes. But punishing yourself forever won't bring her back.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Nathan looks up at Carrie, his eyes filled with anguish.", CharacterNames.Narrator, ""),
            LineBuilder("Then what am I supposed to do? How do I live with this?", CharacterNames.Father, "Nathan", expressionName:"cynical"),
            LineBuilder("You honor her. You remember her, not with guilt, but with love. You take the pain you feel and turn it into a promise to be better. For her.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Nathan stares at Carrie, her words slowly sinking in. Tears stream down his face as his expression shifts from despair to a flicker of hope.", CharacterNames.Narrator, ""),
            LineBuilder("Tears stream down his face as his expression shifts from despair to a flicker of hope.", CharacterNames.Narrator, ""),
            LineBuilder("...for her...", CharacterNames.Father, "Nathan", expressionName:"sad"),
            LineBuilder("She wouldn't want you to stay here forever, drowning in regret. She'd want you to find peace.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Nathan's breathing steadies, and he nods slowly, as if a great weight is being lifted from his shoulders.", CharacterNames.Narrator, ""),
            LineBuilder("...you're right. She... she would've wanted that.", CharacterNames.Father, "Nathan", expressionName:"cynical"),
            LineBuilder("The room begins to change—the dim light grows brighter, and the scattered bottles dissolve into light. Nathan looks around, his expression softening.", CharacterNames.Narrator, ""),
            LineBuilder("I'm so sorry, sweetheart. I'll try to make it right, wherever I go from here.", CharacterNames.Father, "Nathan", expressionName:"sad"),
            LineBuilder("Carrie smiles gently as Nathan stands, the light around him intensifying.", CharacterNames.Narrator, ""),
            LineBuilder("Carrie... Thank you. I don't know what happens next, but whatever it is... I think I'm ready.", CharacterNames.Father, "Nathan", expressionName:"normal"),
            LineBuilder("Nathan steps into the light, his form fading. Carrie watches, her smile bittersweet.", CharacterNames.Narrator, "", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFatherEnd()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder(".....", CharacterNames.Carrie, "Carrie"),
            LineBuilder("He's... gone.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("He left a piece of photo too... looks like the photo is coming together, just missing one more piece.", CharacterNames.Carrie, "Carrie"),
            LineBuilder("...", CharacterNames.Carrie, "Carrie", expressionName:"normal", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    #endregion

    #region Finale events
    public IEnumerator ConvoFinalIntro()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" },
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Well, looks like that was the last door... what happens now?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Wait... what is that? Beyond the hallway...", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("There's another door? I don't think it was there earlier.", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("I should probably check to see if there's someone else that needs help!", CharacterNames.Carrie, "Carrie", expressionName:"happy", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }

    public IEnumerator ConvoFinal1()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Seraph"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried" },
            new InitialExpression { character = CharacterNames.Seraph, expressionName = "grin" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Um... where am I?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Hey there, Carrie.", CharacterNames.Seraph, "???"),
            LineBuilder("Woah!! Who are YOU?", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("Come on, really! It's me, your best friend in limbo! You know, the guy that called you on the phone?", CharacterNames.Seraph, "???", expressionName:"talking"),
            LineBuilder("Oh, wow! I didn't expect you to look so...", CharacterNames.Carrie, "Carrie"),
            LineBuilder("Anyway, you know why you're here don't you? You saved the last batch of people.", CharacterNames.Seraph, "Seraph"),
            LineBuilder("And your scrapbook...", CharacterNames.Seraph, "Seraph", expressionName:"grin"),
            LineBuilder("Oh yeah! I think there's still a piece missing, though...", CharacterNames.Carrie, "Carrie", expressionName:"oo"),
            LineBuilder("Well, now that you finished your final tasks...", CharacterNames.Seraph, "Seraph"),
            LineBuilder("Why don't you open it up again and see?", CharacterNames.Seraph, "Seraph", expressionName:"talking", soundName:"ScrapObtain", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFinal2()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Seraph"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Seraph, expressionName = "grin" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Seeing the completed photo, at that moment, Carrie's memories suddenly started flooding back", CharacterNames.Narrator, ""),
            LineBuilder("... I... my...", CharacterNames.Carrie, "Carrie", expressionName:"surprised"),
            LineBuilder("It might be a little too much for you to handle right now. But you'll have an eternity to get used to it when you're in Heaven.", CharacterNames.Seraph, "Seraph"),
            LineBuilder(".....", CharacterNames.Carrie, "Carrie", expressionName:"worried2"),
            LineBuilder("Well, would you look at that! You got some familiar visitors.", CharacterNames.Seraph, "Seraph", expressionName:"talking"),
            LineBuilder("I'll leave you to them. Thanks for all your hard work till now! See you in the skies! Byeeeeeeeeeeeeeeee!", CharacterNames.Seraph, "Seraph", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFinal3()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Brother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried2" },
            new InitialExpression { character = CharacterNames.Brother, expressionName = "happy" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Hey! Carrie! Over here!", CharacterNames.Brother, "Austin"),
            LineBuilder(".....", CharacterNames.Carrie, "Carrie", expressionName:"worried2"),
            LineBuilder("Well, that's kind of embarassing. Looks like you did better than me in life.", CharacterNames.Brother, "Austin", expressionName:"tired"),
            LineBuilder("But I bet you still can't beat me at my games when we're in heaven! In moderation, of course.", CharacterNames.Brother, "Austin", expressionName:"smug"),
            LineBuilder("...Austin...", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFinal4()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Mother"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried2" },
            new InitialExpression { character = CharacterNames.Mother, expressionName = "openeyestalking" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Carrie! Oh, I'm so sorry.", CharacterNames.Mother, "Linda"),
            LineBuilder("I would never forget you, my daughter. That cup you gave me, I never let go of it.", CharacterNames.Mother, "Linda"),
            LineBuilder("I'm okay now. Oh, but this doesn't mean you can go around making messes wherever you like in Heaven!", CharacterNames.Mother, "Linda", expressionName:"thinking"),
            LineBuilder("...Mama...", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    public IEnumerator ConvoFinal5()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Father"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "worried2" },
            new InitialExpression { character = CharacterNames.Father, expressionName = "normal" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("Well, if it isn't my little treasure, Carrie.", CharacterNames.Father, "Nathan"),
            LineBuilder("I'm sorry for all the things I can't undo. But I know you forgive me.'", CharacterNames.Father, "Nathan"),
            LineBuilder("Just know... papa is very, very proud of his little girl.", CharacterNames.Father, "Nathan", expressionName:"cynical"),
            LineBuilder("...Papa, I...", CharacterNames.Carrie, "Carrie", isFinal:true),
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }

    public IEnumerator ConvoSecretEnding()
    {
        presentCharacters = new List<GameObject>{
            GameObject.Find("Narrator"),
            GameObject.Find("Seraph"),
            GameObject.Find("Carrie")
        };
        dialogueEvent.initialExpressions = new List<InitialExpression>
        {
            new InitialExpression { character = CharacterNames.Carrie, expressionName = "oo" },
            new InitialExpression { character = CharacterNames.Seraph, expressionName = "grin" }
        };
        dialogueEvent.dialogueLines = new List<DialogueLine>
        {
            LineBuilder("...whereas disregard and contempt for human rights have resulted...", CharacterNames.Seraph, "???"),
            LineBuilder("Um... where am I?", CharacterNames.Carrie, "Carrie"),
            LineBuilder("???", CharacterNames.Seraph, "???"),
            LineBuilder("Woah! Yo! You should NOT! Be here right now! How did you even find this??", CharacterNames.Seraph, "???", expressionName:"talking"),
            LineBuilder("Come back when you're done. Pretend you never saw anything!", CharacterNames.Seraph, "???"),
            LineBuilder("Whuh? But, I...", CharacterNames.Carrie, "Carrie", expressionName:"ouo", isFinal:true)
        };
        yield return StartCoroutine(dialogueManager.StartDialogueEvent(dialogueEvent, presentCharacters));
    }
    #endregion
}
