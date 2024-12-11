using UnityEngine;

public class FadeInImage : MonoBehaviour
{
    // Reference to the CanvasGroup attached to the image
    public CanvasGroup canvasGroup;

    // Duration of the fade effect
    public float fadeDuration = 3f;

    // Boolean to track if the fade has started
    private bool isFading = false;

    // The target alpha value (fully visible)
    private float targetAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the image is initially hidden
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // Make it invisible
            canvasGroup.interactable = false; // Optionally disable interaction while faded
            canvasGroup.blocksRaycasts = false; // Prevent the UI from blocking raycasts while faded
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the fade has started, handle the fading effect
        if (isFading)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime / fadeDuration);

            // Check if the alpha value has reached the target, stop fading
            if (canvasGroup.alpha == targetAlpha)
            {
                isFading = false;
                canvasGroup.interactable = true; // Re-enable interaction once fully visible
                canvasGroup.blocksRaycasts = true; // Allow raycasting again
            }
        }
    }

    // Call this function to start the fade-in effect
    public void FadeIn()
    {
        isFading = true;
    }
}
