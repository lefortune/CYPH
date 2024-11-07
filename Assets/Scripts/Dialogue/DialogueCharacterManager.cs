using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueCharacterManager : MonoBehaviour
{
    public CharacterNames characterName;
    public SpriteRenderer characterSprite;
    public Vector3 focusedScale;
    public Vector3 unfocusedScale;
    private Color focusedColor = Color.white;
    private Color unfocusedColor = Color.grey;
    private static float transitionDuration = 0.3f;

    private Vector3 targetScale;
    private Color targetColor;

    public GameObject angryExpressionPrefab;

    private void Start()
    {
        focusedScale = transform.localScale;
        unfocusedScale = focusedScale * 0.9f;

        characterSprite = GetComponent<SpriteRenderer>();
        if (characterSprite == null) {
            Debug.LogError($"SpriteRenderer not found on the GameObject named {characterName}");
        }

        SetUnfocused();
    }

    public void SetFocused()
    {
        if (characterName == CharacterNames.Narrator) return;
        targetScale = focusedScale;
        targetColor = focusedColor;
        StartCoroutine(Transition());
    }

    public void SetUnfocused()
    {
        if (characterName == CharacterNames.Narrator) return;
        targetScale = unfocusedScale;
        targetColor = unfocusedColor;
        StartCoroutine(Transition());
    }

    private System.Collections.IEnumerator Transition()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Color startColor = characterSprite.color;

        while (elapsedTime < transitionDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / transitionDuration);
            characterSprite.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        characterSprite.color = targetColor;
    }

    public void ChangeExpression(Sprite expression)
    {
        if (characterName == CharacterNames.Narrator) return;
        characterSprite.sprite = expression;
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
        else if (action.Contains("angry")) 
        {
            StartCoroutine(AngryCoroutine(gameObject));
        }
        //  Handle other actions (oscillation, animation) if u want
        else {
            Debug.Log("Unknown action, check your dialogue event");
            return;
        }
    }

    #region Actions
    private IEnumerator FadeInCoroutine(Vector3 targetPosition, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.localPosition;
        Color initialColor = characterSprite.color;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, progress);
            characterSprite.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(initialColor.a, endAlpha, progress));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and color are set
        transform.localPosition = targetPosition;
        characterSprite.color = new Color(initialColor.r, initialColor.g, initialColor.b, endAlpha);
    }

    private IEnumerator ShakeCoroutine()
    {
        float shakeDuration = 0.4f;
        float shakeMagnitude = 0.1f;
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition; // Reset to original position
    }
    private IEnumerator HopCoroutine()
    {
        int hopCount = 2;
        float hopHeight = 0.6f;
        float halfHopDuration = 0.1f;
        Vector3 originalPosition = transform.localPosition;

        for (int i = 0; i < hopCount; i++) // Two hops
        {
            // Hop up
            float elapsed = 0f;
            while (elapsed < halfHopDuration)
            {
                transform.localPosition = Vector3.Lerp(originalPosition, originalPosition + Vector3.up * hopHeight, elapsed / halfHopDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            // down
            elapsed = 0f;
            while (elapsed < halfHopDuration)
            {
                transform.localPosition = Vector3.Lerp(originalPosition + Vector3.up * hopHeight, originalPosition, elapsed / halfHopDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
        transform.localPosition = originalPosition; // Reset to original position
    }
    private IEnumerator AngryCoroutine(GameObject target)
    {
        Instantiate(angryExpressionPrefab);
        StartCoroutine(angryExpressionPrefab.GetComponent<AngryExpression>().Initialize(target));
        yield return null;
    }
    #endregion
}
