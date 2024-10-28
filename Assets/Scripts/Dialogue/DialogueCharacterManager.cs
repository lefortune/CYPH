using UnityEngine;
using UnityEngine.UI;

public class DialogueCharacterManager : MonoBehaviour
{
    public CharacterNames characterName;
    public SpriteRenderer characterSprite;
    public Vector3 focusedScale;
    public Vector3 unfocusedScale;
    private Color focusedColor = Color.white;
    public Color unfocusedColor = Color.grey;
    public static float transitionDuration = 0.3f;

    private Vector3 targetScale;
    private Color targetColor;

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
        if (expression == null) {
            return;
        } 
        characterSprite.sprite = expression;
    }

    public void DoAction(string action) 
    {
        if (characterName == CharacterNames.Narrator) return;
        if (action == "none") {
            return;
        } 
        //  Handle other actions (oscillation, animation) if u want
        else {
            Debug.Log("Unknown action, check your dialogue event");
            return;
        }
    }
}
