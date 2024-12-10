using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDoor : MonoBehaviour
{
    private GameObject targetObject;

    void Start()
    {
        targetObject = gameObject;
        CheckVisibility();
    }

    void Update()
    {
        CheckVisibility();
    }

    private void CheckVisibility()
    {
        if (CutscenesManager.scrapPieces >= 3)
        {
            if (!targetObject.activeSelf)
            {
                targetObject.SetActive(true);
            }
        }
        else
        {
            if (targetObject.activeSelf)
            {
                targetObject.SetActive(false);
            }
        }
    }
}
