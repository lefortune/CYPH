using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
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
        backgroundPosY = GameObject.Find("GameMap/Background").transform.position.y;
        backgroundScaleY = GameObject.Find("GameMap/Background").transform.localScale.y;
        height = 2.0f * Camera.main.orthographicSize;
        lowerBound = backgroundPosY - backgroundScaleY / 2 + height/2;
        upperBound = backgroundPosY + backgroundScaleY / 2 - height/2;
    }

    void Update()
    {
        
        transform.position = new Vector3(offset.x, GetPositionY() + offset.y, offset.z);
    }

    float GetPositionY()
    {
        float y = player.position.y <= lowerBound ? lowerBound : player.position.y >= upperBound ? upperBound : player.position.y;
        return y;
    }

}
