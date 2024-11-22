using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BrotherController : MonoBehaviour
{
    private Rigidbody2D brotherRB;
    public Rigidbody2D carrieRB;
    private bool touchingGround;
    private float runSpeed;
    private bool started;
    public Transform player;
    private Animator animator;
    private float elapsedTime;

    int FloorLayer;



    private void Awake()
    {
        brotherRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        started = false;
        runSpeed = 5;
        FloorLayer = LayerMask.NameToLayer("Floor");
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (!started)
        {
            started = !started;
            StartCoroutine(Move(0));
        }
    }

    public IEnumerator Move(int level)
    {
        switch (level)
        {
            case 0:
                while (brotherRB.position.x <= -4.2)
                {
                    Walk();
                    yield return null;
                }
                Jump();
                yield return new WaitForSeconds(.1f);
                yield return new WaitUntil(() => touchingGround);
                while (brotherRB.position.x <= 13)
                {
                    Walk();
                    yield return null;
                }
                Stop();
                player.GetComponent<CarrierBrotherController>().setTransitioning(false);
                break;
            case 1:
                while (brotherRB.position.x <= 14)
                {
                    Walk();
                    yield return null;
                }
                Jump();
                yield return new WaitForSeconds(.1f);
                yield return new WaitUntil(() => touchingGround);
                while (brotherRB.position.x <= 20.5)
                {
                    Walk();
                    yield return null;
                }
                Jump();
                yield return new WaitForSeconds(.1f);
                yield return new WaitUntil(() => touchingGround);
                while (brotherRB.position.x <= 33)
                {
                    Walk();
                    yield return null;
                }
                Stop();
                player.GetComponent<CarrierBrotherController>().setTransitioning(false);
                break;
            case 2:
                yield return new WaitForSeconds(.5f);
                while (brotherRB.position.x <= 24.59)
                {
                    Walk();
                    yield return null;
                }
                Stop();
                yield return new WaitForSeconds(.3f);
                while (brotherRB.position.x <= 27.59)
                {
                    Walk();
                    yield return null;
                }
                Stop();
                yield return new WaitForSeconds(.3f);
                while (brotherRB.position.x <= 53)
                {
                    Walk();
                    yield return null;
                }
                player.GetComponent<CarrierBrotherController>().setTransitioning(false);
                break;
            case 3:
                player.GetComponent<CarrierBrotherController>().setTransitioning(false);
                break;
        }

        yield return null;
    }

    private void Walk()
    {
        if (touchingGround)
        {
            animator.SetBool("Running", true);
        }
        brotherRB.velocity = new Vector2(runSpeed, 0);
    }

    private void Jump()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", true);
        brotherRB.AddForce(new Vector2(0, 300));
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
            animator.SetBool("Jumping", false);
            touchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (isFloor(coll.gameObject))
        {
            animator.SetBool("Running", false);
            touchingGround = false;
        }
    }


}





// Old controls
//if (brotherRB.position.x >= 120)
//{
//    isRunning = false;
//}
//else if (brotherRB.position.x - carrieRB.position.x > 12 && touchingGround)
//{
//    Stop();
//    isRunning = false;
//}
//else
//{
//    isRunning = true;
//}


//if (brotherRB.position.x - carrieRB.position.x > 7)
//{
//    runSpeed = 4;
//}
//else if (brotherRB.position.x - carrieRB.position.x < 5 || brotherRB.position.x >= 100)
//{
//    runSpeed = 6;
//}
//else
//{
//    runSpeed = 5;
//}

//if (isRunning)
//{
//    jumpTimer += Time.deltaTime;

//    if (touchingGround && brotherRB.velocity.x != runSpeed)
//    {
//        Walk();
//    }
//    switch (runSpeed)
//    {
//        case 4:
//            jumpSpot = jumpPosition[jumpSwitch];
//            break;
//        case 5:
//            jumpSpot = jumpPosition[jumpSwitch];
//            break;
//        case 6:
//            jumpSpot = jumpPosition[jumpSwitch] - 5;
//            break;
//    }
//    //Debug.Log(jumpSpot);
//    if (brotherRB.position.x >= jumpSpot && touchingGround)
//    {
//        Jump();
//        jumpSwitch++;
//    }
//}
//else
//{
//    Stop();
//}
