using System;
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
    public GameObject gameManager;

    #region Physics_components
    Rigidbody2D PlayerRB;
    Vector2 currDirection;
    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Camera Controls
    public GameObject mainCamera;
    private bool transitioning;
    #endregion

    #region levelVariable
    int level;
    public List<float> levelEnds = new List<float>();
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

        if (!transitioning)
        {
            Move();
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (PlayerRB.position.y < -10)
        {
            RestartLevel();
        }

        if (PlayerRB.position.x > levelEnds[level])
        {
            transitioning = true;
            gameManager.GetComponent<BrotherGameManager>().NextLevel();
        }
    }
    #endregion

    #region Start
    private void Start()
    {
        transitioning = true;
        level = 1;
    }
    #endregion
    #region Movement_functions
    private void Move()
    {
        Vector2 movement = new Vector2(x_input * moveSpeed, 0);
        PlayerRB.AddForce(movement);

        if (PlayerRB.velocity.x > maxSpeed)
        {
            PlayerRB.transform.rotation = new Quaternion(0,0,0,0);
            PlayerRB.velocity = new Vector2(maxSpeed, PlayerRB.velocity.y);
            atMaxSpeed = true;
        }
        else if (PlayerRB.velocity.x < -maxSpeed)
        {
            PlayerRB.transform.rotation = new Quaternion(0, 180, 0, 0);
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
            anim.SetBool("Jumping", true);
            PlayerRB.AddForce(new Vector2(0, jumpForce));
            StartCoroutine(Delayed());
        }

        if (movement != Vector2.zero)
        {
            currDirection = movement.normalized;
        }

        if (movement == Vector2.zero)
        {
            anim.SetBool("Running", false);
        }
        else
        {
            anim.SetBool("Running", true);
        }

    }

    private IEnumerator Delayed()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Jumping", false);
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
            anim.SetBool("Jumping", false);
            touchingGround = true;
        }

        if (coll.gameObject.CompareTag("Brother"))
        {
            EndGame();
        }

        if (coll.gameObject.CompareTag("Death"))
        {
            RestartLevel();
        }
    }


    void OnCollisionExit2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            anim.SetBool("Running", false);
            touchingGround = false;
        }
    }

    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void RestartLevel()
    {
        PlayerRB.velocity = Vector2.zero;
        PlayerRB.position = new Vector3(levelEnds[level - 1] + .5f, -3.5f);
    }

    public void IncrementLevel()
    {
        level++;
        return;
    }


    public void EndGame()
    {
        SceneManager.LoadScene("BrotherRoom");
    }

    public void setTransitioning(bool t)
    {
        transitioning = t;
        return;
    }

}
