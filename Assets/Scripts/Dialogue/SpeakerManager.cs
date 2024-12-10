using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpeakerManager : MonoBehaviour
{
    public CharacterNames characterName;
    public Image characterImage; // Reference to the Image component
    public RectTransform rectTransform; // To control size (scale)
    private Vector3 originalScale;
    private Color originalColor;

    public float fadeDuration = 0.3f;
    public float unfocusedScaleFactor = 0.9f;
    public Color unfocusedColor = Color.gray;

    void Awake()
    {
        if (characterImage == null) characterImage = GetComponent<Image>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalColor = characterImage.color;

        // Start hidden but active
        characterImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        rectTransform.localScale = originalScale * unfocusedScaleFactor;
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, originalScale, originalColor));
    }

    public void Hide()
{
    characterImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    rectTransform.localScale = originalScale * unfocusedScaleFactor;
}

    public void Focus()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, originalScale, originalColor));
    }

    public void Unfocus()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, originalScale * unfocusedScaleFactor, unfocusedColor));
    }

    private IEnumerator Fade(float targetAlpha, Vector3 targetScale, Color targetColor)
    {
        float elapsed = 0f;
        float currentAlpha = characterImage.color.a;
        Vector3 currentScale = rectTransform.localScale;
        Color currentColor = characterImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            rectTransform.localScale = Vector3.Lerp(currentScale, targetScale, t);
            characterImage.color = Color.Lerp(currentColor, new Color(targetColor.r, targetColor.g, targetColor.b, targetAlpha), t);

            yield return null;
        }

        rectTransform.localScale = targetScale;
        characterImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, targetAlpha);
    }


    public void ChangeExpression(Sprite expression)
    {
        if (characterName == CharacterNames.Narrator) return;
        characterImage.sprite = expression;
    }

    public void DoAction(string action)
    {
        if (characterName == CharacterNames.Narrator) return;
        if (action == "none") {
            return;
        } 
        else if (action.Contains("shake"))
        {
            StartCoroutine(ShakeCoroutine());
        }
        else if (action.Contains("hop")) 
        {
            StartCoroutine(HopCoroutine());
        }
        //  Handle other actions (oscillation, animation) if u want
        else {
            Debug.Log("Unknown action, check your dialogue event");
            return;
        }
    }

    #region Actions
    private IEnumerator ShakeCoroutine()
    {
        float shakeDuration = 0.4f;
        float shakeMagnitude = 10f;
        Vector2 originalPosition = rectTransform.anchoredPosition;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);
            rectTransform.anchoredPosition = originalPosition + new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = originalPosition; 
    }

    private IEnumerator HopCoroutine()
    {
        int hopCount = 2;
        float hopHeight = 20f; // Adjust to suit UI scale (pixels)
        float halfHopDuration = 0.1f;
        Vector2 originalPosition = rectTransform.anchoredPosition;

        for (int i = 0; i < hopCount; i++) // Two hops
        {
            // Hop up
            float elapsed = 0f;
            while (elapsed < halfHopDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, originalPosition + Vector2.up * hopHeight, elapsed / halfHopDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Hop down
            elapsed = 0f;
            while (elapsed < halfHopDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(originalPosition + Vector2.up * hopHeight, originalPosition, elapsed / halfHopDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        rectTransform.anchoredPosition = originalPosition; // Reset to original position
    }

    #endregion
}
