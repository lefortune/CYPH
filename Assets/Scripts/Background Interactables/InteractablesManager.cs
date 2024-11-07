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

            Debug.Log("Starting object interaction");

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
    public void InteractCouch()
    {
        interactableTexts.interactableLines = new List<InteractableTextLine>
        {
            LineBuilder("This couch is really big! \nI've fallen asleep here a couple of times, waiting for my next client.", true)
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
    #endregion

}
