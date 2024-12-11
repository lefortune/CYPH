using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public GameObject text;
    public GameObject textBox;
    public bool isInteractionActive = false;

    public InteractableTexts interactableTexts;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        // text box is already set inactive by DialogueGameManager
    }

    void Update() {
        if (text == null) {
            text = GameObject.Find("DialogueText");
        }
        if (textBox == null) {
            textBox = GameObject.Find("DialogueBox");
        }
    }

    private InteractableTextLine LineBuilder(
        string text,
        bool isFinal = false) 
    {
        return new InteractableTextLine 
        { 
            text = text, 
            isFinal = isFinal
        };
    }

    IEnumerator InteractTextRoutine(InteractableTexts texts)
    {
        isInteractionActive = true;
        textBox.SetActive(true);

        int currLineIndex = 0;
        while (currLineIndex < texts.interactableLines.Count) 
        {
            var line = texts.interactableLines[currLineIndex];

            yield return StartCoroutine(text.GetComponent<DialogueTranslator>().TypeDialogue(line.text));
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0));

            if (line.isFinal)
            {
                break;
            } else {
                currLineIndex++;
            }
        }
        textBox.SetActive(false);
        isInteractionActive = false;
    }


    // Below are all of the Object Interactions in the Game
    #region Object interactions

    // General
    public void InteractScrap()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("You picked up a photo scrap! I wonder what this is a part of... \nCheck your collected photo scraps by pressing \"F\"!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractLeaveEarly()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("There's something that looks important. I should go check it first.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }

    // Main Hallway
    public void InteractCouch()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("So glad the seraphim let me get my own couch in my favorite color. It's so big and cozy! \nI've fallen asleep here a couple of times, waiting for my next client.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractPhone()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("A pretty old-fashioned phone that I can't call anyone on. This is where I get my orders from the seraphim!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractChairs()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Chairs, neatly in a line. \nThere's a bunch, but I don't think I've ever seen anyone sitting here...", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractPlant()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("It's a pink flower! \nI heard it's a forget-me-not, but...are they usually this big and pink?", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractJosh()
    {
        FindAnyObjectByType<AudioManager>().Play("Whistle");
    }
    public void InteractBroDoorEarly()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Hey! What am I doing? I need to answer the phone!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractDoorEarly()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("I better help the door on the left first—keep things organized!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractDoorAfter()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Well, I saved them already, but maybe I'll go back in for old times' sake."),
            LineBuilder("Wait, what? The room... is gone??", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }

    // Brother Room
    public void InteractBed()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("This must be his bed. Pretty messy! There's also a... what was it called again? \"Daki-makura\"?"),
            LineBuilder("That girl on it... I wonder how old she is.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractTV()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("There's a video game on... I see a small, flying white character, and next to her there's a orange-haired man holding a blue and watery weapon."),
            LineBuilder("He looks cool! I guess that's a break from anime women for once.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractCouchBro()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Aside from the crumbs and stains, it's in relatively good condition. \nI prefer mine though!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractToybox()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Honestly, I'm a little scared to open this and see what \"toys\" are inside...", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractDirtyClothes()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("I'm going to be sick.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractCloset()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("The shelves are filled to the brim with figurines and collectibles. \nSaying this feels wrong, but... it's really quite an impressive collection!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractDeskBro()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Quite the setup he's got going on here! \nHey, this object tucked away on the left... looks really familiar for some reason?", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }

    // Mother Room
    public void InteractIsland()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("That's a cool island! Kitchen islands are awesome.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractMiniWindow()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("You can't see anything out the window! Must be cause we're still in limbo.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractFridge()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("No alcohol in the fridge, surprisingly!"),
            LineBuilder("Unless someone else already took it all...", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }

    // Father Room
    public void InteractClock()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("The hands aren't moving. They're stuck on 3:53.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractCouchDad()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Well, I'd certainly take Austin's couch over this thing.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractGarbage()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("The bags are all filled with... Food! Thank Goodness."),
            LineBuilder("They surprisingly dont stink... The perks of being in purgatory, I guess.", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    public void InteractBottles()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("Geez... how can a person drink this much alcohol? The bottles are all empty!", true)
        };
        StartCoroutine(InteractTextRoutine(interactableTexts));
    }
    #endregion

}
