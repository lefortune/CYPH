using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherGameManager : MonoBehaviour
{

    int level;
    public GameObject mainCamera;
    public GameObject carrie;
    public GameObject smashers;
    public GameObject brother;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        level++;
        carrie.GetComponent<CarrierBrotherController>().IncrementLevel();
        mainCamera.GetComponent<BrotherCameraFollow>().IncrementLevel();

        switch (level)
        {
            case 2:
                for (int i = 0; i < smashers.transform.childCount; i++)
                {
                    StartCoroutine(smashers.transform.GetChild(i).GetComponent<SmasherScript>().Move(false));
                }
                break;
            case 3:
                for (int i = 0; i < smashers.transform.childCount; i++)
                {
                    StopCoroutine(smashers.transform.GetChild(i).GetComponent<SmasherScript>().Move(false));
                }
                break;

        }
    }


}
