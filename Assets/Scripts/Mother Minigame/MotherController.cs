using UnityEngine;
using System.Collections;

public class MotherController : MonoBehaviour
{
    public GameObject Carrie;
    public Rigidbody2D MotherRB;
    public float speed;
    private bool cleaning;
    private bool moving;
    private float elapsedTime;
    private float elapsedTimeTwo;
    private int turnTime;

    private void Update()
    {
        if (!moving)
        {
            moving = true;
            StartCoroutine(CleaningLoop());
        }
    }

    private void Start()
    {
        moving = false;
        cleaning = true;
    }

    public IEnumerator CleaningLoop()
    {
        turnTime = Random.Range(4, 7);
        elapsedTime = 0;
        while (cleaning)
        {
            while (MotherRB.position.x < 3.8)
            {
                MotherRB.velocity = Vector2.right * speed;
                elapsedTime += Time.deltaTime;
                if (elapsedTime > turnTime)
                {
                    StartCoroutine(turn());
                    StopCoroutine(CleaningLoop());
                    break;
                }
                yield return null;
            }
            while (MotherRB.position.x > -3.8)
            {
                MotherRB.velocity = Vector2.left * speed;
                elapsedTime += Time.deltaTime;
                if (elapsedTime > turnTime)
                {
                    StartCoroutine(turn());
                    StopCoroutine(CleaningLoop());
                    break;
                }
                yield return null;
            }
            if (elapsedTime > turnTime)
            {
                break;
            }
            yield return null;
        }
        yield return null;
    }


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
                Debug.Log("You Lose!");
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
        while (true)
        {
            elapsedTimeTwo = 0;
            while (elapsedTimeTwo < 2)
            {
                MotherRB.rotation += .03f;
                elapsedTimeTwo += Time.deltaTime;
                yield return null;
            }
            elapsedTimeTwo = 0;
            while (elapsedTimeTwo < 2)
            {
                MotherRB.rotation -= .03f;
                elapsedTimeTwo += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

}
