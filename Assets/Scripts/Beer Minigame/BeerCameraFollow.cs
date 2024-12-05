using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);
    float backgroundPosY;
    float backgroundPosX;
    float backgroundScaleY;
    float backgroundScaleX;
    float lowerBound;
    float upperBound;
    float rightBound;
    float leftBound;
    float height;


    private void Start()
    {
        backgroundPosY = GameObject.Find("GameMap/Background").transform.position.y;
        backgroundScaleY = GameObject.Find("GameMap/Background").transform.localScale.y;
        backgroundPosX = GameObject.Find("GameMap/Background").transform.position.x;
        backgroundScaleX = GameObject.Find("GameMap/Background").transform.localScale.x;
        height = 2.0f * Camera.main.orthographicSize;
        lowerBound = backgroundPosY - backgroundScaleY / 2 + height/2;
        upperBound = backgroundPosY + backgroundScaleY / 2 - height/2;
        rightBound = backgroundPosX + backgroundScaleX / 2 - height / 2;
        leftBound = backgroundPosX - backgroundScaleX / 2 + height / 2;
    }

    void Update()
    {
        
        transform.position = new Vector3(GetPositionX() + offset.x, GetPositionY() + offset.y, offset.z);
    }

    float GetPositionY()
    {
        float y = player.position.y <= lowerBound ? lowerBound : player.position.y >= upperBound ? upperBound : player.position.y;
        return y;
    }

    float GetPositionX()
    {
        float y = player.position.x <= leftBound ? leftBound : player.position.x >= rightBound ? rightBound : player.position.x;
        return y;
    }

}
