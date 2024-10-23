using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierBeerController : MonoBehaviour
{
    #region Movement_variables
    float moveSpeed = 3;
    float x_input;
    float y_input;
    bool isSprint = false;
    float backgroundPosY;
    float backgroundScaleY;
    float backgroundPosX;
    float backgroundScaleX;
    Vector2 carrieSize;
    float upperBound;
    float lowerBound;
    float rightBound;
    float leftBound;
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
    }
    #endregion

    #region Start
    private void Start()
    {
        backgroundPosY = GameObject.Find("GameMap/Background").transform.position.y;
        backgroundPosX = GameObject.Find("GameMap/Background").transform.position.x;
        backgroundScaleY = GameObject.Find("GameMap/Background").transform.localScale.y;
        backgroundScaleX = GameObject.Find("GameMap/Background").transform.localScale.x;
        carrieSize = GetComponent<SpriteRenderer>().bounds.size;
        upperBound = backgroundPosY + backgroundScaleY / 2 - carrieSize.y / 2;
        lowerBound = backgroundPosY - backgroundScaleY / 2 + carrieSize.y / 2;
        rightBound = backgroundPosX + backgroundScaleX / 2 - carrieSize.x / 2;
        leftBound = backgroundPosX - backgroundScaleX / 2 + carrieSize.x / 2;

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


        if (PlayerRB.transform.position.y >= upperBound && PlayerRB.velocity.y > 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, 0);
        }
        else if (PlayerRB.transform.position.y <= lowerBound && PlayerRB.velocity.y < 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, 0);
        }

        if (PlayerRB.transform.position.x >= rightBound && PlayerRB.velocity.x > 0)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
        }
        else if (PlayerRB.transform.position.x <= leftBound && PlayerRB.velocity.x < 0)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
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

    #region triggers
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colloding");
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Won!");
        }
    }
    #endregion
}
