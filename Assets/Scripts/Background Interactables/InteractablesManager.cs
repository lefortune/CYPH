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
        // text box is already set inactive by DialogueGameManager
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0));

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

    // Brother Room
    
    #endregion

}
