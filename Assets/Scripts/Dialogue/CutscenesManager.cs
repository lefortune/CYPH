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
    public static int scrapPieces;


    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        dialogueEvents = GetComponent<DialogueEvents>();
        inEvent = false;
        cutsceneNum = 0;
        scrapPieces = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueEvents == null) {
            dialogueEvents = GetComponent<DialogueEvents>();
        }

        if (cutsceneNum == 0 && SceneManager.GetActiveScene().name == "ExplorationScene" && !inEvent) {
            inEvent = true;
            cutsceneNum = 1;
            StartCoroutine(dialogueEvents.ConvoIntro1());      // ik this doesnt wait for the coroutine but, "if it works..."
        }

        if (!inEvent && cutsceneNum == 2 && SceneManager.GetActiveScene().name == "BrotherRoom") {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoEnterBroRoom());
            cutsceneNum = 3;
        }
        if (!inEvent && cutsceneNum == 5 && SceneManager.GetActiveScene().name == "BrotherRoom") {
            inEvent = true;
            StartCoroutine(CutsceneConvoBrotherLeaving());
            cutsceneNum = 6;
        }

        if (!inEvent && cutsceneNum == 6 && SceneManager.GetActiveScene().name == "MotherRoom") {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoEnterMomRoom());
            cutsceneNum = 7;
        }
        if (!inEvent && cutsceneNum == 9 && SceneManager.GetActiveScene().name == "MotherRoom") {
            inEvent = true;
            StartCoroutine(CutsceneConvoMotherLeaving());
            cutsceneNum = 10;
        }

        if (!inEvent && cutsceneNum == 11 && SceneManager.GetActiveScene().name == "FatherRoom") {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoEnterDadRoom());
            cutsceneNum = 12;
        }
        if (!inEvent && cutsceneNum == 14 && SceneManager.GetActiveScene().name == "FatherRoom") {
            inEvent = true;
            StartCoroutine(CutsceneConvoFatherLeaving());
            cutsceneNum = 15;
        }

        if (!inEvent && cutsceneNum == 15 && SceneManager.GetActiveScene().name == "ExplorationScene") {
            inEvent = true;
            StartCoroutine(dialogueEvents.ConvoFinalIntro());
            cutsceneNum = 16;
        }

        if (!inEvent && cutsceneNum < 16 && SceneManager.GetActiveScene().name == "FinalScene") {
            inEvent = true;
            StartCoroutine(CutsceneFinalSecret());
        }

        if (!inEvent && cutsceneNum < 99 && cutsceneNum >= 16 && SceneManager.GetActiveScene().name == "FinalScene") {
            inEvent = true;
            StartCoroutine(CutsceneFinal());
            cutsceneNum = 99;
            return;
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
        SceneTransition.Instance.TransitionToScene("Brother Minigame");
        cutsceneNum = 4;
    }

    public IEnumerator CutsceneConvoBrotherCaught()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoBrother2());
        Debug.Log("first don");
        yield return StartCoroutine(FindAnyObjectByType<AsunaBackgroundFade>().TransitionToBlack());
        Debug.Log("second don");
        yield return StartCoroutine(dialogueEvents.ConvoBrother3());
        Debug.Log("third don");
        cutsceneNum = 5;
        SceneTransition.Instance.TransitionToScene("BrotherRoom");
    }

    public IEnumerator CutsceneConvoBrotherLeaving()
    {
        inEvent = true;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FindAnyObjectByType<RoomBrotherLeave>().RoomBroWalkToDoor());
        yield return StartCoroutine(dialogueEvents.ConvoBrother4());
        inEvent = true;
        yield return StartCoroutine(FindAnyObjectByType<RoomBrotherLeave>().BrotherMoveOutFade());
        yield return StartCoroutine(dialogueEvents.ConvoBrotherEnd());
    }

    public IEnumerator CutsceneConvoMother()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoMother1());
        SceneTransition.Instance.TransitionToScene("Mother Minigame");
        cutsceneNum = 8;
    }

    public IEnumerator CutsceneConvoMotherCaught()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoMother2());
        cutsceneNum = 9;
        SceneTransition.Instance.TransitionToScene("MotherRoom");
    }

    public IEnumerator CutsceneConvoMotherLeaving()
    {
        inEvent = true;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FindAnyObjectByType<RoomBrotherLeave>().RoomBroWalkToDoor());
        yield return StartCoroutine(FindAnyObjectByType<RoomBrotherLeave>().BrotherMoveOutFade());
        yield return StartCoroutine(dialogueEvents.ConvoMotherEnd());
    }

    public IEnumerator CutsceneEnterDadBefore()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoEnterDadBefore());
        SceneTransition.Instance.TransitionToScene("FatherRoom");
        cutsceneNum = 11;
    }

    public IEnumerator CutsceneConvoFather()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoFather1());
        SceneTransition.Instance.TransitionToScene("Dad Minigame");
        cutsceneNum = 13;
    }

    public IEnumerator CutsceneConvoFatherCaught()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoFather2());
        Debug.Log("first don");
        yield return new WaitForSeconds(1f);
        Debug.Log("second don");
        yield return StartCoroutine(dialogueEvents.ConvoFather3());
        Debug.Log("third don");
        cutsceneNum = 14;
        SceneTransition.Instance.TransitionToScene("FatherRoom");
    }

    public IEnumerator CutsceneConvoFatherLeaving()
    {
        inEvent = true;
        FindAnyObjectByType<RoomBrotherLeave>().LiterallyJustGone();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(dialogueEvents.ConvoFatherEnd());
    }

    public IEnumerator CutsceneFinal()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoFinal1());
        Debug.Log("first don");
        scrapPieces = 4;
        yield return StartCoroutine(FindAnyObjectByType<ViewScrapbook>().ShowBook());
        Debug.Log("second don");
        yield return StartCoroutine(dialogueEvents.ConvoFinal2());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(dialogueEvents.ConvoFinal3());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(dialogueEvents.ConvoFinal4());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(dialogueEvents.ConvoFinal5());
        yield return new WaitForSeconds(1f);
        FindAnyObjectByType<FadeInImage>().FadeIn();
    }

    public IEnumerator CutsceneFinalSecret()
    {
        inEvent = true;
        yield return StartCoroutine(dialogueEvents.ConvoSecretEnding());
        SceneTransition.Instance.TransitionToScene("ExplorationScene");

    }



}
