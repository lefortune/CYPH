using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueCharacterManager : MonoBehaviour
{
    public CharacterNames characterName;
    public SpriteRenderer characterImage;
    public Vector3 focusedScale;
    public Vector3 unfocusedScale;
    private Color focusedColor = Color.white;
    private Color unfocusedColor;
    private static float transitionDuration = 0.3f;

    private Vector3 targetScale;
    private Color targetColor;

    public GameObject angryExpressionPrefab;

    private void Awake()
    {
        focusedScale = transform.localScale;
        unfocusedScale = focusedScale * 0.9f;

        characterImage = GetComponent<SpriteRenderer>();

        if (characterImage == null) {
            Debug.LogError($"SpriteRenderer not found on the GameObject named {characterName}");
        }

    }

    private void Start()
    {
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
        Color originalColor = characterImage.color;
        unfocusedColor = new Color(0.5f, 0.5f, 0.5f, originalColor.a);
        targetScale = unfocusedScale;
        targetColor = unfocusedColor;
        StartCoroutine(Transition());
    }

    private System.Collections.IEnumerator Transition()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Color startColor = characterImage.color;

        while (elapsedTime < transitionDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / transitionDuration);
            characterImage.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        characterImage.color = targetColor;
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
        else if (action.Contains("angry")) 
        {
            StartCoroutine(AngryCoroutine(gameObject));
        }
        else if (action.Contains("fadein")) 
        {
            StartCoroutine(FadeCoroutine(-2f, 0f, true));
        }
        //  Handle other actions (oscillation, animation) if u want
        else {
            Debug.Log("Unknown action, check your dialogue event");
            return;
        }
    }

    #region Actions
    private IEnumerator FadeCoroutine(float startX, float targetX, bool fromLeft)
    {
        float elapsedTime = 0f;
        float duration = 0.3f;
        Color initialColor = characterImage.color;
        initialColor.a = 0f;
        Vector3 currentPosition = transform.localPosition;
        currentPosition.x = startX;
        transform.localPosition = currentPosition;
        characterImage.color = initialColor;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            currentPosition.x = Mathf.Lerp(startX, targetX, progress);
            transform.localPosition = currentPosition;
            characterImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(0f, 1f, progress));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentPosition.x = targetX;
        transform.localPosition = currentPosition;
        characterImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
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

    public void Invisible(bool tf) {
        Color a = characterImage.color;
        a.a = tf ? 0f : 1f;
        characterImage.color = a;
    }

}
