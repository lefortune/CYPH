using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherCameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform brother;
    public Vector3 offset = new Vector3(0, 0, -10);
    float height;
    int level;
    float moveDuration = 2f;
    float elapsedTime;
    public List<float> positions = new List<float>();


    private void Start()
    {
        level = 0;
    }

    void Update()
    {

    }

    IEnumerator MoveTo(int level)
    {
        elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(new Vector3(positions[level], 0, -10), new Vector3(positions[level + 1], 0, -10), elapsedTime/ moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(brother.GetComponent<BrotherController>().Move(level + 1));
    }

    public void IncrementLevel()
    {
        StartCoroutine(MoveTo(level));
        level++;
    }
}
