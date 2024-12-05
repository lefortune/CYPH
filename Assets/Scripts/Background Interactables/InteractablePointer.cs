using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePointer : MonoBehaviour
{
    public GameObject targetObject;  // The GameObject that will appear/disappear.
    public float appearanceDistance = 5f;  // The distance at which the targetObject will appear.
    public Transform playerTransform;  // The player character's transform.

    private Renderer targetRenderer;

    public bool appear;

    void Start()
    {
        targetRenderer = targetObject.GetComponent<Renderer>();
        SetVisibility(false);
    }

    void Update()
    {
        
    }

    public void SetVisibility(bool isVisible)
    {
        if (targetRenderer != null)
        {
            targetRenderer.enabled = isVisible;
        }
    }
}
