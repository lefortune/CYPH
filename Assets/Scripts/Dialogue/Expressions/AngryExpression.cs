using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryExpression : MonoBehaviour
{
    public IEnumerator Initialize(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 offset = new Vector3(target.GetComponent<Renderer>().bounds.size.x / 2, 
                                      target.GetComponent<Renderer>().bounds.size.y / 2, 0);
        transform.position = targetPosition + offset;

        // Fade in
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        float fadeDuration = 1f;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        renderer.material.color = targetColor;

        // Shake
        Vector3 originalPosition = transform.position;
        float shakeDuration = 0.5f;
        float shakeMagnitude = 0.1f;
        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            float x = originalPosition.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalPosition.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(x, y, originalPosition.z);
            yield return null;
        }
        transform.position = originalPosition;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        Destroy(gameObject);
    }
}
