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
            if (CutscenesManager.cutsceneNum == 1) {
                interactablesManager.InteractDoorEarly();
            } else if (CutscenesManager.cutsceneNum == 2) {
                SceneTransition.Instance.TransitionToScene("BrotherRoom");
            } else {
                interactablesManager.InteractDoorAfter();
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
        if (objName == "RoomLeave")
        {
            SceneTransition.Instance.TransitionToScene("ExplorationScene");
        }
        if (objName == "BrotherInteractable")
        {
            StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoBrother());
        }
    }
}
