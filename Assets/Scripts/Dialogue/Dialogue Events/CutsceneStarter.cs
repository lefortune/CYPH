using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneStarter : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public static bool inEvent;
    public static int cutsceneNum;


    void Awake() 
    {
        dialogueEvents = GetComponent<DialogueEvents>();
        cutsceneNum = 0;
    }
    void Start()
    {
        inEvent = true;
        if (SceneManager.GetActiveScene().name == "ExplorationScene") {
            dialogueEvents.IntroCutscenePt1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inEvent && cutsceneNum == -1) {
            SceneManager.LoadScene("Brother Minigame");
        }
    }

    public void IntroCutscenePt2Start()
    {
        inEvent = true;
        FindAnyObjectByType<AudioManager>().Stop("PhoneLinging");
        FindAnyObjectByType<AudioManager>().Play("PhonePickup");
        dialogueEvents.IntroCutscenePt2();
    }

    public void ConvoBrother1Start()
    {
        inEvent = true;
        cutsceneNum = -2;
        dialogueEvents.ConvoBrother_1();
    }

}
