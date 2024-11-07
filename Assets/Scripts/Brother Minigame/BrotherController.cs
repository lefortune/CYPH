using System.Collections.Generic;
using UnityEngine;

public class BrotherController : MonoBehaviour
{
    private Rigidbody2D brotherRB;
    public Rigidbody2D carrieRB;
    private bool isRunning;
    private float jumpTimer;
    private int actionStage;
    private bool jumping;
    private bool touchingGround;
    public List<float> jumpPosition = new List<float>();
    private int jumpSwitch;
    private float runSpeed;
    private float jumpSpot;

    int FloorLayer;



    private void Awake()
    {
        brotherRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        runSpeed = 5;
        isRunning = true;
        actionStage = 0;
        jumpTimer = 0f;
        //makeJumpTimes();
        jumpSwitch = 0;
        FloorLayer = LayerMask.NameToLayer("Floor");

    }

    private void Update()
    {

        //Debug.Log(brotherRB.position.x);
        if (brotherRB.position.x >= 120)
        {
            isRunning = false;
        }
        else if (brotherRB.position.x - carrieRB.position.x > 20 && touchingGround)
        {
            Stop();
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }


        if (brotherRB.position.x - carrieRB.position.x > 10 || brotherRB.position.x >= 100)
        {
            runSpeed = 4;
        }
        else if (brotherRB.position.x - carrieRB.position.x < 5)
        {
            runSpeed = 6;
        }
        else
        {
            runSpeed = 5;
        }


        Debug.Log(runSpeed);

        if (isRunning)
        {
            jumpTimer += Time.deltaTime;

            if (touchingGround && brotherRB.velocity.x != runSpeed)
            {
                Walk();
            }
            switch (runSpeed)
            {
                case 4:
                    jumpSpot = jumpPosition[jumpSwitch];
                    break;
                case 5:
                    jumpSpot = jumpPosition[jumpSwitch];
                    break;
                case 6:
                    jumpSpot = jumpPosition[jumpSwitch] - 5;
                    break;
            }
            Debug.Log(jumpSpot);
            if (brotherRB.position.x >= jumpSpot && touchingGround)
            {
                Jump();
                jumpSwitch++;
            }
        }
        else
        {
            Stop();
        }



    }

    private void Walk()
    {
        brotherRB.velocity = new Vector2(runSpeed, 0);
    }

    private void Jump()
    {
        brotherRB.AddForce(new Vector2(0, 500));
    }

    private void Stop()
    {
        brotherRB.velocity = Vector2.zero;
    }

    bool isFloor(GameObject obj)
    {
        return obj.layer == FloorLayer;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            touchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            touchingGround = false;
        }
    }


}
