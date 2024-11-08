using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStarter : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public static bool inEvent;
    public static int cutsceneNum;

    public CarrieController playerController;

    void Awake() 
    {
        playerController = FindAnyObjectByType<CarrieController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inEvent = true;
        dialogueEvents = GetComponent<DialogueEvents>();
        cutsceneNum = 0;
        dialogueEvents.IntroCutscenePt1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IntroCutscenePt2Start()
    {
        inEvent = true;
        FindAnyObjectByType<AudioManager>().Stop("PhoneLinging");
        FindAnyObjectByType<AudioManager>().Play("PhonePickup");
        dialogueEvents.IntroCutscenePt2();
    }

}
