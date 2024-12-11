using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundInteractables : MonoBehaviour
{
    string objName;
    InteractablesManager interactablesManager;

    void Awake()
    {
        objName = gameObject.name;
        interactablesManager = FindAnyObjectByType<InteractablesManager>();
    }

    public void Interact()
    {
        if (objName == "PhotoScrap") 
        {
            interactablesManager.InteractScrap();
            gameObject.GetComponent<ScrapGeneric>().scrapPickedUp();
        }
        
        // Main Hallway
        if (objName == "BGCouch") 
        {
            interactablesManager.InteractCouch();
        }
        if (objName == "BGChairs") 
        {
            interactablesManager.InteractChairs();
        }
        if (objName == "BGPlant") 
        {
            interactablesManager.InteractPlant();
        }
        if (objName == "BGJosh")
        {
            FindAnyObjectByType<AudioManager>().Play("Josh");
        }
        if (objName == "BGPhone")
        {
            if (CutscenesManager.cutsceneNum == 1) {
                StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoIntroPhone());
            } else {
                interactablesManager.InteractPhone();
            }
        }

        if (objName == "Door1")
        {
            if (CutscenesManager.cutsceneNum < 2) {
                interactablesManager.InteractBroDoorEarly();
            } else if (CutscenesManager.cutsceneNum >= 2 && CutscenesManager.scrapPieces < 1) {
                SceneTransition.Instance.TransitionToScene("BrotherRoom");
            } else {
                interactablesManager.InteractDoorAfter();
            }
        }
        if (objName == "Door2")
        {
            if (CutscenesManager.cutsceneNum < 6) {
                interactablesManager.InteractDoorEarly();
            } else if (CutscenesManager.cutsceneNum >= 6 && CutscenesManager.scrapPieces < 2) {
                SceneTransition.Instance.TransitionToScene("MotherRoom");
            } else {
                interactablesManager.InteractDoorAfter();
            }
        }
        if (objName == "Door3")
        {
            if (CutscenesManager.cutsceneNum < 10) {
                interactablesManager.InteractDoorEarly();
            } else if (CutscenesManager.cutsceneNum >= 10 && CutscenesManager.scrapPieces < 3) {
                StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneEnterDadBefore());
            } else {
                interactablesManager.InteractDoorAfter();
            }
        }

        if (objName == "RoomLeave")
        {
            if (CutscenesManager.cutsceneNum >= 6 && CutscenesManager.scrapPieces < 1 && SceneManager.GetActiveScene().name == "BrotherRoom") {
                interactablesManager.InteractLeaveEarly();
            } else if (CutscenesManager.cutsceneNum >= 10 && CutscenesManager.scrapPieces < 2 && SceneManager.GetActiveScene().name == "MotherRoom") {
                interactablesManager.InteractLeaveEarly();
            } else if (CutscenesManager.cutsceneNum >= 15 && CutscenesManager.scrapPieces < 3 && SceneManager.GetActiveScene().name == "FatherRoom") {
                interactablesManager.InteractLeaveEarly();
            } else {
                SceneTransition.Instance.TransitionToScene("ExplorationScene");
            }
        }

        // Brother's Room
        if (objName == "BGBed") 
        {
            interactablesManager.InteractBed();
        }
        if (objName == "BGTV") 
        {
            interactablesManager.InteractTV();
        }
        if (objName == "BGCouchBro") 
        {
            interactablesManager.InteractCouchBro();
        }
        if (objName == "BGToybox") 
        {
            interactablesManager.InteractToybox();
        }
        if (objName == "BGDirtyClothes") 
        {
            interactablesManager.InteractDirtyClothes();
        }
        if (objName == "BGCloset") 
        {
            interactablesManager.InteractCloset();
        }
        if (objName == "BGDeskBro") 
        {
            interactablesManager.InteractDeskBro();
        }
        if (objName == "BrotherInteractable")
        {
            StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoBrother());
        }

        // Mother
        if (objName == "BGIsland") 
        {
            interactablesManager.InteractIsland();
        }
        if (objName == "BGMiniWindow") 
        {
            interactablesManager.InteractMiniWindow();
        }
        if (objName == "BGFridge") 
        {
            interactablesManager.InteractFridge();
        }
        if (objName == "MotherInteractable")
        {
            StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoMother());
        }

        // Father
        if (objName == "BGClock") 
        {
            interactablesManager.InteractClock();
        }
        if (objName == "BGCouchDad") 
        {
            interactablesManager.InteractCouchDad();
        }
        if (objName == "BGGarbage") 
        {
            interactablesManager.InteractGarbage();
        }
        if (objName == "BGBottles") 
        {
            interactablesManager.InteractBottles();
        }
        if (objName == "FatherInteractable")
        {
            StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoFather());
        }

        // Final
        if (objName == "DoorFinal")
        {
            SceneTransition.Instance.TransitionToScene("FinalScene");
        }
    }
}
