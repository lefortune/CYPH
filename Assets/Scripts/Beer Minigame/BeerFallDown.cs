using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerFallDown : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Beer Speed")]
    private float beerSpeed = 2;

    [SerializeField]
    [Tooltip("Beer Rotation Speed")]
    private float beerRotationSpeed = .3f;

    private float backgroundPosY;
    private float backgroundScaleY;
    private float lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        backgroundPosY = GameObject.Find("GameMap/Background").transform.position.y;
        backgroundScaleY = GameObject.Find("GameMap/Background").transform.localScale.y;
        lowerBound = backgroundPosY - backgroundScaleY / 2 - 5;

        GetComponent<Rigidbody2D>().AddForce(Vector2.down * beerSpeed * 10);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1f, 1f) * beerRotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.y);
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }

    #region triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Lost!");
        }
    }
    #endregion
}
