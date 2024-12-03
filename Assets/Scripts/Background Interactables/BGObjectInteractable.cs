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
            if (CutsceneStarter.cutsceneNum == 1) {
                FindAnyObjectByType<CutsceneStarter>().IntroCutscenePt2Start();
            } else {
                interactablesManager.InteractPhone();
            }
        }
        if (objName == "Door1")
        {
            CutsceneStarter.cutsceneNum = 2;
            SceneManager.LoadScene("BrotherRoom");
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
            CutsceneStarter.cutsceneNum = 3;
            SceneManager.LoadScene("ConvoScene");
        }
    }
}
