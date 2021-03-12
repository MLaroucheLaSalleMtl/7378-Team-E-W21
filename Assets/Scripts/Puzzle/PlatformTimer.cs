using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTimer : MonoBehaviour
{
    private float timeLimit = 5;
    private float timer;
    private bool playerOn = false;
    private bool hasKey = false;

    public bool HasKey { get => hasKey; set => hasKey = value; }

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerOn)
        {
            playerOn = false;
        }
    }

    private void CountdownTimer()
    {
        if (playerOn)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeLimit;
        }

        if (timer == 0)
        {
            hasKey = true;
        }
    }
}
