using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScrapbook : MonoBehaviour
{
    // Reference to the Canvas component
    private Canvas canvas;
    public static bool isScrapbookOpen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Get the Canvas component attached to this GameObject
        canvas = GetComponent<Canvas>();

        // Initially hide the Canvas
        if (canvas != null)
        {
            canvas.enabled = false;
            isScrapbookOpen = false;
        }
    }

    private void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the enabled state of the Canvas
            if (canvas != null)
            {
                canvas.enabled = !canvas.enabled;
                isScrapbookOpen = !isScrapbookOpen;
            }

            if (canvas.enabled)
            {
                ScrapPieceShow[] scrapPieces = GetComponentsInChildren<ScrapPieceShow>();
                foreach (ScrapPieceShow scrapPiece in scrapPieces)
                {
                    // Toggle the visibility based on the name of the GameObject
                    scrapPiece.ToggleVisibilityBasedOnName();
                }
                FindObjectOfType<AudioManager>().Play("PaperOpen");
            }
        }
    }
}
