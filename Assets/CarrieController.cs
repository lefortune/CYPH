using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrieController : MonoBehaviour
{
    #region Movement_variables
    float moveSpeed = 3;
    float x_input;
    float y_input;
    bool isSprint = false;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    Vector2 currDirection;
    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Unity_functions
    private void Awake() {
        PlayerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Update() {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) {
            isSprint = true;
        } else {
            isSprint = false;
        }
        Move();

        if (Input.GetKeyDown(KeyCode.F)) {
            Interact();
        }
    }
    #endregion

    #region Movement_functions
    private void Move()
    {

        Vector2 movement = new Vector2(x_input, y_input) * moveSpeed;
        PlayerRB.velocity = movement;
        if (isSprint) {
            PlayerRB.velocity *= 2;
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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(0.5f, 0.5f), 0f, Vector2.zero, 0f);
        foreach(RaycastHit2D hit in hits) {
            if (hit.transform.CompareTag("Chest")) {
//                hit.transform.GetComponent<Chest>().Open();
            }
        }
    }


    #endregion
}
