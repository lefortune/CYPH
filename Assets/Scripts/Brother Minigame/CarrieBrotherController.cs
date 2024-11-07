using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarrierBrotherController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    float x_input;
    float y_input;
    bool isSprint = false;
    //float backgroundPosY;
    //float backgroundScaleY;
    //float backgroundPosX;
    //float backgroundScaleX;
    public float jumpForce;
    public float maxSpeed;
    bool touchingGround = false;
    bool atMaxSpeed = false;
    #endregion

    #region Boundary_variables
    Vector2 carrieSize;
    float upperBound;
    float lowerBound;
    float rightBound;
    float leftBound;
    #endregion

    int FloorLayer;

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
        FloorLayer = LayerMask.NameToLayer("Floor");
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

        if (PlayerRB.position.y < -100)
        {
            RestartScene();
        }
    }
    #endregion

    #region Start
    private void Start()
    {

    }
    #endregion
    #region Movement_functions
    private void Move()
    {
        Vector2 movement = new Vector2(x_input * moveSpeed, 0);
        PlayerRB.AddForce(movement);
        if (isSprint) {
            PlayerRB.velocity *= 2;
        }

        if (PlayerRB.velocity.x > maxSpeed)
        {
            PlayerRB.velocity = new Vector2(maxSpeed, PlayerRB.velocity.y);
            atMaxSpeed = true;
        }
        else if (PlayerRB.velocity.x < -maxSpeed)
        {
            PlayerRB.velocity = new Vector2(-maxSpeed, PlayerRB.velocity.y);
            atMaxSpeed = true;

        }
        else
        {
            atMaxSpeed = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
        {
            //PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, 0);
            PlayerRB.AddForce(new Vector2(0, jumpForce));
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

    bool isFloor(GameObject obj)
    {
        return obj.layer == FloorLayer;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            if (atMaxSpeed)
            {
                PlayerRB.velocity = new Vector2(maxSpeed, PlayerRB.velocity.y);
            }
            touchingGround = true;
        }

        if (coll.gameObject.CompareTag("Brother"))
        {
            EndGame();
        }
    }


    void OnCollisionExit2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            touchingGround = false;
        }
    }

    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void EndGame()
    {
        Debug.Log("You Win!");
    }

}
