using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MotherController : MonoBehaviour
{
    public GameObject Carrie;
    public Rigidbody2D MotherRB;
    public Animator anim;
    public float speed;
    private bool cleaning;
    private bool moving;
    private float elapsedTime;
    private float elapsedTimeTwo;
    private int turnTime;

    public GameObject lineOfSight;

    private void Update()
    {
        Debug.Log(lineOfSight.transform.rotation);
        Debug.Log(MotherRB.velocity.x > 0);
        if (MotherRB.velocity.x > 0)
        {
            lineOfSight.transform.rotation = Quaternion.Euler(0, 0, 90);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Moving", true);
            anim.SetBool("Right", true);
        }
        else if (MotherRB.velocity.x < 0)
        {
            lineOfSight.transform.rotation = Quaternion.Euler(0, 0, -90);
            anim.SetBool("Left", true);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Moving", true);
            anim.SetBool("Right", false);
        }
        else if (MotherRB.velocity.y > 0)
        {
            lineOfSight.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim.SetBool("Left", false);
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Moving", true);
            anim.SetBool("Right", false);
        }
        else if (MotherRB.velocity.y < 0)
        {
            lineOfSight.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Moving", true);
        }
        else 
        {
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Moving", false);
        }

        if (!moving)
        {
            moving = true;
            //StartCoroutine(CleaningLoop());
            StartCoroutine(Sweep());
        }
    }

    private void Start()
    {
        //lineOfSight.GetComponent<SpriteRenderer>().enabled = false;
        moving = false;
        cleaning = true;
        anim = GetComponent<Animator>();
    }

    public IEnumerator Sweep()
    {
        while (MotherRB.position.x > -2)
        {
            MotherRB.velocity = Vector2.left * speed;
            yield return null;
        }
        MotherRB.velocity = Vector2.zero;
        lineOfSight.GetComponentInChildren<SpriteRenderer>().enabled = false;
        anim.SetBool("Sweep", true);
        yield return new WaitForSeconds(1);
        MotherRB.transform.rotation = new Quaternion(0, 180, 0, 0);
        yield return new WaitForSeconds(1);
        MotherRB.transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return new WaitForSeconds(1);
        MotherRB.transform.rotation = new Quaternion(0, 180, 0, 0);
        yield return new WaitForSeconds(1);
        MotherRB.transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetBool("Sweep", false);
        lineOfSight.GetComponentInChildren<SpriteRenderer>().enabled = true;
        lineOfSight.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(3);
        while (MotherRB.position.x < 1.5)
        {
            MotherRB.velocity = Vector2.right * speed;
            yield return null;
        }
        MotherRB.velocity = Vector2.zero;
        lineOfSight.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(3);
        while (MotherRB.position.x > -2)
        {
            MotherRB.velocity = Vector2.left * speed;
            yield return null;
        }
        MotherRB.velocity = Vector2.zero;
        while (MotherRB.position.y > -2)
        {
            MotherRB.velocity = Vector2.down * speed;
            yield return null;
        }
        MotherRB.velocity = Vector2.zero;

    }

    //public IEnumerator CleaningLoop()
    //{
    //    //turnTime = Random.Range(4, 7);
    //    turnTime = 3;
    //    elapsedTime = 0;
    //    while (cleaning)
    //    {
    //        while (MotherRB.position.x < 3.4)
    //        {
    //            MotherRB.velocity = Vector2.right * speed;
    //            elapsedTime += Time.deltaTime;
    //            if (elapsedTime > turnTime)
    //            {
    //                StartCoroutine(turn());
    //                StopCoroutine(CleaningLoop());
    //                break;
    //            }
    //            yield return null;
    //        }
    //        while (MotherRB.position.x > -3.4)
    //        {
    //            MotherRB.velocity = Vector2.left * speed;
    //            elapsedTime += Time.deltaTime;
    //            if (elapsedTime > turnTime)
    //            {
    //                StartCoroutine(turn());
    //                StopCoroutine(CleaningLoop());
    //                break;
    //            }
    //            yield return null;
    //        }
    //        if (elapsedTime > turnTime)
    //        {
    //            break;
    //        }
    //        yield return null;
    //    }
    //    yield return null;
    //}


    public IEnumerator turn()
    {
        MotherRB.velocity = Vector2.zero;
        while (MotherRB.rotation < 140)
        {
            MotherRB.rotation += .06f;
            yield return null;
        }
        StartCoroutine(Search());
        yield return null;
    }

    public IEnumerator Search()
    {
        elapsedTime = 0;
        StartCoroutine(Look());
        while (true)
        {
            if (Carrie.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                Scene currentScene = SceneManager.GetActiveScene();

                SceneManager.LoadScene(currentScene.name);
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 8)
            {
                StopAllCoroutines();
                break;
            }
            yield return null;
        }

    }

    public IEnumerator Look()
    {
        //while (true)
        //{
        //    elapsedTimeTwo = 0;
        //    while (elapsedTimeTwo < 2)
        //    {
        //        MotherRB.rotation += .03f;
        //        elapsedTimeTwo += Time.deltaTime;
        //        yield return null;
        //    }
        //    elapsedTimeTwo = 0;
        //    while (elapsedTimeTwo < 2)
        //    {
        //        MotherRB.rotation -= .03f;
        //        elapsedTimeTwo += Time.deltaTime;
        //        yield return null;
        //    }
        //    yield return null;
        //}

        yield return null;
    }

}
