using UnityEngine;
using System.Collections;

public class ShardScript : MonoBehaviour
{
    public GameObject gameManager;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GetComponent<BeerGameManager>().restartGame();
        }
    }

}
