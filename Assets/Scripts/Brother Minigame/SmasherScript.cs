using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherScript : MonoBehaviour
{
    private float elapsedTime;
    public float upDuration;
    public float downDuration;
    public float maxHeight;
    public float minHeight;
    public float topWaitTime;
    public float botWaitTime;
    public float startDelay;
    private Vector3 topPos;
    private Vector3 botPos;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        botPos = new Vector3(this.GetComponent<Rigidbody2D>().position.x, minHeight, 0);
        topPos = new Vector3(this.GetComponent<Rigidbody2D>().position.x, maxHeight, 0);
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Move(bool up)
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            elapsedTime = 0;
            if (up)
            {
                while (elapsedTime < upDuration)
                {
                    transform.position = Vector3.Lerp(botPos, topPos, elapsedTime / upDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                transform.position = topPos;
            }
            else
            {
                while (elapsedTime < downDuration)
                {
                    transform.position = Vector3.Lerp(topPos, botPos, elapsedTime / downDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                transform.position = botPos;
            }
            if (up)
            {
                yield return new WaitForSeconds(topWaitTime);
            }
            else
            {
                yield return new WaitForSeconds(botWaitTime);
            }
            up = !up;
        }
    }

}
