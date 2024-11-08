using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrieController : MonoBehaviour
{
    #region Movement_variables
    float moveSpeed = 0.05f;
    float x_input;
    float y_input;
    bool isSprint = false;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    BoxCollider2D PlayerColl;
    public Vector2 currDirection;
    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Other_variables
    public InteractablesManager interactablesManager;
    #endregion

    #region Unity_functions
    private void Awake() {
        PlayerRB = GetComponent<Rigidbody2D>();
        PlayerColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        interactablesManager = FindAnyObjectByType<InteractablesManager>();
    }
    
    private void Update() {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) {
            isSprint = true;
        } else {
            isSprint = false;
        }
        if (!interactablesManager.isInteractionActive && !CutsceneStarter.inEvent)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.F)) {
                Interact();
            }
        }

    }
    #endregion

    #region Movement_functions
    private void Move()
    {
        Vector2 movement = new Vector2(x_input, y_input) * moveSpeed;
        PlayerRB.MovePosition(PlayerRB.position + movement);
        if (isSprint) {
            moveSpeed = 0.1f;
            anim.speed = 2;
        } else {
            moveSpeed = 0.05f;
            anim.speed = 1;
        }

        if (movement != Vector2.zero)
        {
            currDirection = movement.normalized;
        }

        if (movement == Vector2.zero)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);

        }

        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        Vector2 colliderCenter = PlayerColl.bounds.center;
        Vector2 colliderSize = PlayerColl.bounds.size;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            colliderCenter + currDirection/2, new Vector2(colliderSize.x/2, colliderSize.y/2), 0f, Vector2.zero, 0f
            );
        float closestDistance = float.MaxValue;
        RaycastHit2D closestHit = new RaycastHit2D();

        foreach(RaycastHit2D hit in hits) {
            if (hit.transform.CompareTag("Interactable")) {
                float distance = Vector2.Distance(colliderCenter + currDirection/2, hit.point);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHit = hit;
                }
                Debug.Log("Starting object interaction");
                closestHit.transform.GetComponent<BackgroundInteractables>().Interact();
            }
        }
    }

    #endregion
}
