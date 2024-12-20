using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGameManager : MonoBehaviour
{
    public GameObject BEER;
    [SerializeField]
    [Tooltip("Time Between Beer Spawns")]
    private float beerTime = 5;

    [SerializeField]
    [Tooltip("First Beer Spawn")]
    private float startTime = 5;

    private bool beerToggle = false;

    private float beerTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (DaddyController.embraced)
        {
            return;
        }
        
        if (beerToggle)
        {
            if (beerTimer <= 0)
            {
                beerTimer = beerTime;
                SpawnBeer();
            }
            else
            {
                beerTimer -= Time.deltaTime;
            }
        }
        else if (startTime <= 0)
        {
            beerToggle = true;
        }
        else
        {
            startTime -= Time.deltaTime;
        }
    }

    void SpawnBeer()
    {
        Vector3 randomizePosition = new Vector3(40, Random.Range(-5, 5), 0);

        //update the y value depending on how much you want the thing to randomly rotate
        Quaternion rotation = new Quaternion(0, 0, 0, 0);

        GameObject Beer = Instantiate(BEER, randomizePosition, rotation);
    }

    void DestroyAllObjectsByTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }


    public void restartGame()
    {
        GameObject.Find("PlayerCarrie").transform.position = new Vector3(0, 0, 0);
        DestroyAllObjectsByTag("BEER");
    }
}
