using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUMMYSCRIPT : MonoBehaviour
{
    public DialogueEvents dialogueEvents;
    // Start is called before the first frame update
    void Start()
    {
        dialogueEvents = GetComponent<DialogueEvents>();
        dialogueEvents.ConvoBrother_1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
