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


    public GameObject shard1;
    public GameObject shard2;

    public GameObject gameManager;

    Rigidbody2D beerRB;
    private float backgroundPosX;
    private float backgroundScaleX;
    private float lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        backgroundPosX = GameObject.Find("GameMap/Background").transform.position.x;
        backgroundScaleX= GameObject.Find("GameMap/Background").transform.localScale.x;
        lowerBound = backgroundPosX - backgroundScaleX / 2 - 5;

        GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(0f, 1f) * beerSpeed * 10);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1f, 1f) * beerRotationSpeed);

        beerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < lowerBound)
        {
            Destroy(gameObject);
        }
    }

    #region triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Shatter(true);
        }
        else
        {
            Shatter(false);
        }
    }
    #endregion

    void Shatter(bool losing)
    {

        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        Vector2 velocity = beerRB.velocity;

        GameObject Shard1 = Instantiate(shard1, transform.position, rotation);
        GameObject Shard2 = Instantiate(shard2, transform.position, rotation);

        Shard1.GetComponent<ShardScript>().Move((float)(velocity.x * Random.Range(0.5f, 0.75f)), Random.Range(20, 30));
        Shard2.GetComponent<ShardScript>().Move((float)(velocity.x * Random.Range(0.5f, 0.75f)), -Random.Range(20, 30));

        if (losing)
        {
            gameManager.GetComponent<BeerGameManager>().restartGame();
        }
        Destroy(gameObject);
    }
}
