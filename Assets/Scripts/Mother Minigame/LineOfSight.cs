using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Debug.Log("You Lose");
        }

    }
}
