using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBrotherLeave : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isMoving = false;

    public float moveSpeed = 1f;  // Speed at which the character moves
    public float distanceRight = 5f; // Distance to move right
    public float distanceDown = -2.5f;
    public float fadeDuration = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (CutscenesManager.cutsceneNum == 5) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator RoomBroWalkToDoor() {
        animator.SetBool("WalkRight", true); // Trigger the MoveR animation
        Vector3 targetPositionRight = transform.position + new Vector3(distanceRight, 0, 0);

        while (transform.position.x < targetPositionRight.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionRight, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure character has exactly reached the target position
        transform.position = targetPositionRight;

        // Move Down while playing the Move Down animation
        animator.SetBool("WalkRight", false);
        animator.SetBool("WalkDown", true); // Trigger the MoveD animation
        Vector3 targetPositionDown = transform.position + new Vector3(0, -distanceDown, 0);

        while (transform.position.y > targetPositionDown.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionDown, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure character has exactly reached the target position
        transform.position = targetPositionDown;

        // End the movement sequence (optional, you can trigger another animation or do something else here)
        isMoving = false;
        animator.SetBool("WalkDown", false);
        animator.SetBool("Stand", true);
        yield return null;
    }

    public IEnumerator BrotherMoveOutFade()
    {
        // Continue moving down while fading out
        float startTime = Time.time;
        float startAlpha = spriteRenderer.color.a; // Start with the current alpha value

        while (spriteRenderer.color.a > 0f) // While the alpha is greater than 0 (still visible)
        {
            // Move down slowly while fading
            transform.position += Vector3.down * (moveSpeed * Time.deltaTime); // Continue moving down
            float elapsedTime = Time.time - startTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration); // Lerp alpha to 0 over time
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            yield return null; // Wait for the next frame
        }

        // Ensure the character is completely faded out
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        
        // You can optionally destroy or deactivate the character here if needed:
        Destroy(gameObject); // Or use SetActive(false);
    }

}
