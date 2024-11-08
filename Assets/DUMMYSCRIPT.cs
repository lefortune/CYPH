using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUMMYSCRIPT : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    public CutsceneStarter cutsceneStarter;
    // Start is called before the first frame update
    void Start()
    {
        dialogueEvents = GetComponent<DialogueEvents>();
        cutsceneStarter = GetComponent<CutsceneStarter>();
        cutsceneStarter.ConvoBrother1Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
