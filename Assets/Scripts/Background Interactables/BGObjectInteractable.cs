using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
