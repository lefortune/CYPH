using UnityEngine;
using System.Collections;

public class ShardScript : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
    }

    public void Move(float x, float y)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }
}
