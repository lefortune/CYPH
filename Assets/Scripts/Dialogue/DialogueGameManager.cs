using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueGameManager : MonoBehaviour
{
    public GameObject Dialogue;
    private int DialogueNum = 0;
    private int DialogueCheck = -1;
    public List<string> lines = new List<string>();

    // Use this for initialization
    void Start()
    {
        MakeLines();
    }

    void MakeLines()
    {
        lines.Add("The weather outside is rizzy, but the fire is so skibidi. And since I've gyatt to go, ohio ohio ohio.");
        lines.Add("It's beginning to look a gyatt like rizzmass, everywhere online. My sigma level is maxxed, your food got fanum taxxed");
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueNum != DialogueCheck)
        {
            DialogueCheck = DialogueNum;
            StartCoroutine(StartDialogue(DialogueNum));
        }
        //Dialogue.GetComponent<DialogueTranslator>().StartDialogue("The weather outside is rizzy, but the fire is so skibidi. And since I've gyatt to go, ohio ohio ohio.");
    }

    IEnumerator StartDialogue(int num)
    {
        yield return StartCoroutine(Dialogue.GetComponent<DialogueTranslator>().TypeDialogue(lines[num]));
        MakeAnswerOptions();
        //DialogueNum++;
    }

    void MakeAnswerOptions()
    {
        
    }

}
