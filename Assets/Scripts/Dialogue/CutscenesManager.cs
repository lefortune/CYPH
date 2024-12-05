using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenesManager : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public static bool inEvent;
    public static int cutsceneNum;


    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        dialogueEvents = GetComponent<DialogueEvents>();
        inEvent = false;
        cutsceneNum = 0;
    }
    void Start()
    {
        if (cutsceneNum == 0 && SceneManager.GetActiveScene().name == "ExplorationScene" && !inEvent) {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoIntro1());      // ik this doesnt wait for the coroutine but, "if it works..."
            cutsceneNum = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueEvents == null) {
            dialogueEvents = GetComponent<DialogueEvents>();
        }

        if (!inEvent && cutsceneNum == 2 && SceneManager.GetActiveScene().name == "BrotherRoom") {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoEnterBroRoom());
            cutsceneNum = 3;
        }

        // First Brother Dialog call found in BGObjectInteractable

        // Debug.Log(cutsceneNum + " | " + inEvent);
    }

    public IEnumerator CutsceneConvoIntroPhone()
    {
        inEvent = true;
        FindAnyObjectByType<AudioManager>().Stop("PhoneLinging");
        FindAnyObjectByType<AudioManager>().Play("PhonePickup");
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(dialogueEvents.ConvoIntro2());
        cutsceneNum = 2;
    }

    public IEnumerator CutsceneConvoBrother()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoBrother1());
        SceneManager.LoadScene("Brother Minigame");
        cutsceneNum = 4;
    }

    public IEnumerator CutsceneConvoBrotherCaught()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoBrother2());
        cutsceneNum = 5;
    }

}
