using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundInteractables : MonoBehaviour
{
    string objName;

    void Awake()
    {
        objName = gameObject.name;
    }

    public void Interact()
    {
        if (objName == "BGCouch") 
        {
            FindAnyObjectByType<InteractablesManager>().InteractCouch();
        }
        if (objName == "BGChairs") 
        {
            FindAnyObjectByType<InteractablesManager>().InteractChairs();
        }
        if (objName == "BGPlant") 
        {
            FindAnyObjectByType<InteractablesManager>().InteractPlant();
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
                FindAnyObjectByType<InteractablesManager>().InteractPhone();
            }
        }
        if (objName == "Door1")
        {
            CutsceneStarter.cutsceneNum = 2;
            SceneManager.LoadScene("BrotherRoom");
        }

        if (objName == "BrotherInteractable")
        {
            CutsceneStarter.cutsceneNum = 3;
            SceneManager.LoadScene("ConvoScene");
        }
    }
}
