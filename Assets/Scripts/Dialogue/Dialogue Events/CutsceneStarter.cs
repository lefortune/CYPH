using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneStarter : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public static bool inEvent;
    public static int cutsceneNum;


    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        dialogueEvents = GetComponent<DialogueEvents>();
        cutsceneNum = 0;
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ExplorationScene") {
            inEvent = true;
            dialogueEvents.IntroCutscenePt1();
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
            dialogueEvents.BrotherRoomCutscene1();
        }

        if (!inEvent && cutsceneNum == 3 && SceneManager.GetActiveScene().name == "ConvoScene") {
            inEvent = true;
            cutsceneNum = -2;
            dialogueEvents.ConvoBrother_1();
        }

        if (!inEvent && cutsceneNum == -1) {
            cutsceneNum = 0;
            SceneManager.LoadScene("Brother Minigame");
        }
        // Debug.Log(cutsceneNum + " | " + inEvent);
    }

    public void IntroCutscenePt2Start()
    {
        inEvent = true;
        FindAnyObjectByType<AudioManager>().Stop("PhoneLinging");
        FindAnyObjectByType<AudioManager>().Play("PhonePickup");
        dialogueEvents.IntroCutscenePt2();
    }

}
