using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);
    float backgroundPosY;
    float backgroundScaleY;
    float lowerBound;
    float upperBound;
    float height;


    private void Start()
    {
        lowerBound = 0;
    }

    void Update()
    {
        
        transform.position = new Vector3(GetPositionX() + offset.x, GetPositionY() + offset.y, offset.z);
    }

    float GetPositionY()
    {
        float y = player.position.y;
        if (y + offset.y < lowerBound)
        {
            return 0;
        }
        return y;
    }

    float GetPositionX()
    {
        float x = player.position.x;
        return x;

    }

}
