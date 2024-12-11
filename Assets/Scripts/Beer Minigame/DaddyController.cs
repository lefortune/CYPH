using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaddyController : MonoBehaviour
{
    public float delay;
    private float elapsedTime;
    Animator anim;
    public static bool embraced;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        embraced = false;
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
            Debug.Log("You Win");
            embraced = true;
            StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoFatherCaught());
            this.enabled = false;
        }
    }

    public void TryThisInsteadSinceCollisionSucks() {
        Debug.Log("You Win");
        embraced = true;
        StartCoroutine(FindAnyObjectByType<CutscenesManager>().CutsceneConvoFatherCaught());
        this.enabled = false;
    }

}
