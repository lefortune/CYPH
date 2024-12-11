using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrapGeneric : MonoBehaviour
{

    private SpriteRenderer objRenderer;
    
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    // Pulse speed (seconds)
    public float pulseSpeed = 2f;

    // Pulse intensity (how much it fades from dark to light)
    public float pulseIntensity = 0.8f;

    private void Start()
    {
        // Get the Renderer component from the object
        objRenderer = GetComponent<SpriteRenderer>();

        if (CutscenesManager.cutsceneNum < 5 && SceneManager.GetActiveScene().name == "BrotherRoom")
        {
            gameObject.SetActive(false);
        }

        if (CutscenesManager.cutsceneNum < 9 && SceneManager.GetActiveScene().name == "MotherRoom")
        {
            gameObject.SetActive(false);
        }

        if (CutscenesManager.cutsceneNum < 14 && SceneManager.GetActiveScene().name == "FatherRoom")
        {
            gameObject.SetActive(false);
        }

        StartCoroutine(PulseCoroutine());
    }

    // Coroutine to pulse the material's color
    private IEnumerator PulseCoroutine()
    {
        float time = 0f;

        // Continuously pulse between the two colors
        while (true)
        {
            // Calculate a lerp factor based on time
            time += Time.deltaTime * pulseSpeed;

            // Use Mathf.PingPong to smoothly interpolate between 0 and 1
            float lerpValue = Mathf.PingPong(time, 1f);

            // Interpolate between lightColor and darkColor
            Color newColor = Color.Lerp(darkColor, lightColor, lerpValue * pulseIntensity);

            // Apply the new color to the object's material
            objRenderer.material.color = newColor;

            // Wait until the next frame
            yield return null;
        }
    }

    public void scrapPickedUp() 
    {
        FindObjectOfType<AudioManager>().Play("ScrapObtain");
        CutscenesManager.scrapPieces ++;
        Debug.Log(CutscenesManager.scrapPieces);
        Destroy(gameObject);
    }
}
