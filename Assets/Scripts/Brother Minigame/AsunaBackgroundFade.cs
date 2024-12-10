using UnityEngine;
using System.Collections;

public class AsunaBackgroundFade : MonoBehaviour
{
    public SpriteRenderer[] backgroundSprites;
    public float transitionDuration = 2f; 

    void Start()
    {
        if (backgroundSprites.Length == 0)
        {
            backgroundSprites = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    public IEnumerator TransitionToBlack()
    {
        Color targetColor = Color.black;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            foreach (var sprite in backgroundSprites)
            {
                sprite.color = Color.Lerp(sprite.color, targetColor, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var sprite in backgroundSprites)
        {
            sprite.color = targetColor;
        }
    }
}
