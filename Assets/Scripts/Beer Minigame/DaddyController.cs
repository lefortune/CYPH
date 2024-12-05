using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaddyController : MonoBehaviour
{
    public float delay;
    private float elapsedTime;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > delay)
        {
            elapsedTime = 0;
            anim.SetTrigger("Throw");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Won!");
        }
    }

}
